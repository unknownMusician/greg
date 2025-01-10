#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AreYouFruits.Collections;
using AreYouFruits.Nullability;
using UnityEditor;
using UnityEngine;

namespace Greg.Utils.TagSearcher
{
    public sealed class ScriptTagHierarchyNode
    {
        private readonly ScriptTagHierarchyNode[] children;

        public IReadOnlyList<ScriptTagHierarchyNode> Children => children;
        public string Name { get; }

        public ScriptTagHierarchyNode(string name, IEnumerable<ScriptTagHierarchyNode> children)
        {
            this.children = children.ToArray();
            Name = name;
        }
    }
    
    public interface IScriptTagHierarchyNodeBuilder
    {
        public ScriptTagHierarchyNode Build();
    }
    
    public sealed class ScriptTagHierarchyBranchNodeBuilder : IScriptTagHierarchyNodeBuilder
    {
        public List<IScriptTagHierarchyNodeBuilder> Children { get; } = new();
        public Enum Tag { get; set; }

        public ScriptTagHierarchyNode Build()
        {
            return new ScriptTagHierarchyNode(Tag?.ToString(), Children.Select(c => c.Build()));
        }
    }
    public sealed class ScriptTagHierarchyLeafNodeBuilder : IScriptTagHierarchyNodeBuilder
    {
        public Type Type { get; set; }

        public ScriptTagHierarchyNode Build()
        {
            return new ScriptTagHierarchyNode(Type.Name, Array.Empty<ScriptTagHierarchyNode>());
        }
    }

    public sealed class ScriptTagHierarchyWindow : EditorWindow
    {
        private readonly List<Type> tagGroups = new();
        private readonly List<bool> hierarchyPopState = new();

        private Dictionary<Enum, HashSet<Type>> typesByTag;
        private Dictionary<Type, HashSet<Enum>> indexTagsByTagType;
        private HashSet<Type> tagTypes;
        private HashSet<Type> scriptTypes;
        private List<Type> freeTags;
        private ScriptTagHierarchyNode hierarchy;
        private Vector2 scroll;
        
        [MenuItem("Are You Fruits?/Script Tag Hierarchy")]
        private static void Create()
        {
            EditorWindow.GetWindow<ScriptTagHierarchyWindow>("Script Tag Hierarchy");
        }

        private void OnGUI()
        {
            if (typesByTag is null || tagTypes is null)
            {
                (typesByTag, tagTypes, scriptTypes) = IndexSearch();
                freeTags = tagTypes.ToList();
                freeTags.Sort((a, b) => a.Name.CompareTo(b.Name));
                indexTagsByTagType = IndexTagsByTagType();
            }
            
            GUILayout.Label("Tags", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal();
            for (var i = 0; i < tagGroups.Count; i++)
            {
                var backgroundColor = GUI.backgroundColor;
                
                if (GUILayout.Button(tagGroups[i].Name, GUILayout.ExpandWidth(false)))
                {
                    var genericMenu = new GenericMenu();
                    
                    var j = i;

                    foreach (var tagType in tagTypes)
                    {
                        genericMenu.AddItem(new GUIContent($"Change/{tagType.Name}"), false, () =>
                        {
                            tagGroups[j] = tagType;
                        });
                    }

                    AddItemToGenericMenu(genericMenu, new GUIContent("Move/Left"), false, j > 0, () =>
                    {
                        (tagGroups[j], tagGroups[j - 1]) = (tagGroups[j - 1], tagGroups[j]);
                    });
                    
                    AddItemToGenericMenu(genericMenu, new GUIContent("Move/Right"), false, j < tagGroups.Count - 1, () =>
                    {
                        (tagGroups[j], tagGroups[j + 1]) = (tagGroups[j + 1], tagGroups[j]);
                    });
                    
                    genericMenu.AddItem(new GUIContent("Remove"), false, () =>
                    {
                        tagGroups.RemoveAt(j);
                    });
                    
                    genericMenu.ShowAsContext();
                }
                GUI.backgroundColor = backgroundColor;
            }

            if (GUILayout.Button("+", GUILayout.ExpandWidth(false)))
            {
                Type newTag;
                
                if (freeTags.Count > 0)
                {
                    newTag = freeTags[0];
                    freeTags.RemoveAt(0);
                }
                else
                {
                    newTag = tagTypes.First();
                }
                
                tagGroups.Add(newTag);
            }

            EditorGUILayout.EndHorizontal();

            var wasGuiEnabled = GUI.enabled;
            GUI.enabled = tagGroups.Distinct().Count() == tagGroups.Count;
            
            if (GUILayout.Button("Apply tags"))
            {
                hierarchy = BuildHierarchy(tagGroups);
            }

            GUI.enabled = wasGuiEnabled;
            
            GUILayout.Label("Hierarchy", EditorStyles.boldLabel);

            scroll = EditorGUILayout.BeginScrollView(scroll);
            
            if (hierarchy is not null)
            {
                DrawHierarchy(hierarchy);
            }
            
            EditorGUILayout.EndScrollView();
        }

        private static void AddItemToGenericMenu(
            GenericMenu menu,
            GUIContent content,
            bool on,
            bool enabled, 
            GenericMenu.MenuFunction func
        )
        {
            if (enabled)
            {
                menu.AddItem(content, on, func);
            }
            else
            {
                menu.AddDisabledItem(content, on);
            }
        }
        
        private void DrawHierarchy(ScriptTagHierarchyNode hierarchyNode)
        {
            var order = 0;

            foreach (var childHierarchyNode in hierarchyNode.Children)
            {
                order += DrawHierarchyNode(childHierarchyNode, order, true);
            }
        }

        private int DrawHierarchyNode(ScriptTagHierarchyNode hierarchyNode, int order, bool isShown)
            // todo: Recursion to iteration
        {
            if (order >= hierarchyPopState.Count)
            {
                hierarchyPopState.AddRange(Enumerable.Repeat(false, order - hierarchyPopState.Count + 1));
            }

            if (isShown)
            {
                if (hierarchyNode.Children.Any())
                {
                    hierarchyPopState[order] = EditorGUILayout.Foldout(hierarchyPopState[order], hierarchyNode.Name ?? "[Other]");
                }
                else
                {
                    //EditorGUILayout.LabelField(EditorGUIUtility.IconContent("cs Script Icon"), GUILayout.MaxWidth(18));
                    EditorGUILayout.LabelField(hierarchyNode.Name ?? "[Other]", EditorStyles.boldLabel);
                }
            }

            if (!hierarchyPopState[order])
            {
                isShown = false;
            }

            var orderOffset = 1;

            foreach (var childHierarchyNode in hierarchyNode.Children)
            {
                EditorGUI.indentLevel++;
                orderOffset += DrawHierarchyNode(childHierarchyNode, order + orderOffset, isShown);
                EditorGUI.indentLevel--;
            }

            return orderOffset;
        }

        private static (Dictionary<Enum, HashSet<Type>> TypesByTag, HashSet<Type> TagTypes, HashSet<Type> ScriptTypes) 
            IndexSearch()
        {
            var typesByTag = new Dictionary<Enum, HashSet<Type>>();
            var tagTypes = new HashSet<Type>();
            var scriptTypes = new HashSet<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.FullName is not { } typeName)
                    {
                        continue;
                    }

                    if (typeName.Contains('<') && typeName.Contains('>'))
                    {
                        continue;
                    }

                    if (type.GetCustomAttribute<ScriptTagAttribute>() is not { } attribute)
                    {
                        continue;
                    }

                    if (!attribute.Tags.Any(t => t is Enum))
                    {
                        continue;
                    }

                    scriptTypes.Add(type);

                    foreach (var tag in attribute.Tags)
                    {
                        if (tag is Enum enumTag)
                        {
                            typesByTag.GetOrInsertNew(enumTag).Add(type);
                            tagTypes.Add(enumTag.GetType());
                        }
                    }
                }
            }

            return (typesByTag, tagTypes, scriptTypes);
        }

        private Dictionary<Type, HashSet<Enum>> IndexTagsByTagType()
        {
            var tagsByTagType = new Dictionary<Type, HashSet<Enum>>();

            foreach (var tag in typesByTag.Keys)
            {
                tagsByTagType.GetOrInsertNew(tag.GetType()).Add(tag);
            }

            return tagsByTagType;
        }

        private ScriptTagHierarchyNode BuildHierarchy(IReadOnlyList<Type> tagTypes)
        {
            var uncheckedLeafs = new Queue<(List<Type> Types, ScriptTagHierarchyBranchNodeBuilder Parent, int Depth)>();
            
            var root = new ScriptTagHierarchyBranchNodeBuilder();
            
            uncheckedLeafs.Enqueue((new List<Type>(scriptTypes), root, 0));

            while (uncheckedLeafs.TryDequeue(out var entry))
            {
                var entryParent = entry.Parent;
                var entryTypes = entry.Types;
                var entryDepth = entry.Depth;

                if (entryDepth >= tagTypes.Count)
                {
                    foreach (var entryType in entryTypes)
                    {
                        entryParent.Children.Add(new ScriptTagHierarchyLeafNodeBuilder
                        {
                            Type = entryType,
                        });
                    }
                    
                    continue;
                }

                var tagType = tagTypes[entryDepth];
                
                var dictionary = new Dictionary<Optional<Enum>, List<Type>>();

                foreach (var entryType in entryTypes)
                {
                    dictionary.GetOrInsertNew(GetTag(entryType, tagType)).Add(entryType);
                }

                foreach (var (tag, types) in dictionary)
                {
                    var childNode = new ScriptTagHierarchyBranchNodeBuilder
                    {
                        Tag = tag.GetOrDefault(),
                    };
                    
                    entryParent.Children.Add(childNode);
                    entryParent.Children.Sort((a, b) =>
                    {
                        if (a is ScriptTagHierarchyBranchNodeBuilder aBranch
                         && b is ScriptTagHierarchyBranchNodeBuilder bBranch)
                        {
                            return CompareNames(aBranch.Tag?.ToString(), bBranch.Tag?.ToString());
                        }

                        if (a is ScriptTagHierarchyLeafNodeBuilder aLeaf
                         && b is ScriptTagHierarchyLeafNodeBuilder bLeaf)
                        {
                            return CompareNames(aLeaf.Type?.Name, bLeaf.Type?.Name);
                        }

                        if (a is ScriptTagHierarchyBranchNodeBuilder && b is ScriptTagHierarchyLeafNodeBuilder)
                        {
                            return -1;
                        }

                        if (a is ScriptTagHierarchyLeafNodeBuilder && b is ScriptTagHierarchyBranchNodeBuilder)
                        {
                            return 1;
                        }

                        return 0;
                    });
                    
                    uncheckedLeafs.Enqueue((types, childNode, entryDepth + 1));
                }
            }

            return root.Build();
        }

        private static int CompareNames(string lhs, string rhs)
        {
            if (lhs is null)
            {
                return 1;
            }

            if (rhs is null)
            {
                return -1;
            }

            return string.Compare(lhs, rhs);
        }
        
        private Optional<Enum> GetTag(Type scriptType, Type tagType)
        {
            if (!indexTagsByTagType.TryGetValue(tagType, out var tags))
            {
                return Optional.None();
            }

            foreach (var tag in tags)
            {
                if (!typesByTag.TryGetValue(tag, out var typesWithTag))
                {
                    continue;
                }

                if (typesWithTag.Contains(scriptType))
                {
                    return tag;
                }
            }
            
            return Optional.None();
        }
    }
}
#endif