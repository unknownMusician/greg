#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AreYouFruits.Collections;
using UnityEditor;
using UnityEngine;

namespace Greg.Utils.TagSearcher
{
    public sealed class ScriptTagSearcherWindow : EditorWindow
        // todo: A bit unreliable script parsing.
        // todo: Decompose.
    {
        private const int MaxTypesCount = 500; 
        private const int MaxTagsCount = 500; 
        private const string TypeFilterKey = "ScriptTagSearcherWindow_TypeFilter"; 
        private const string ShouldIncludeAutoTagsKey = "ScriptTagSearcherWindow_ShouldIncludeAutoTags"; 
        private const string TagFilterKey = "ScriptTagSearcherWindow_TagFilter"; 
        
        private readonly HashSet<string> selectedTags = new();
        
        private Dictionary<string, HashSet<Type>> indexedTypes;
        private Dictionary<Type, HashSet<string>> indexedTags;
        private string[] filteredTags;
        private Type[] filteredTypes;
        
        private string tagFilter;
        private string typeFilter;
        private string lastTypeFilter;
        private string lastTagFilter;
        private Vector2 typesScrollPosition;
        private Vector2 tagsScrollPosition;
        private bool shouldIncludeAutoTags;
        private bool lastShouldIncludeAutoTags;
        private int? selectedTagGroup;
        private Dictionary<string, int> groupsByTags = new();
        private List<List<string>> tagGroups = new();

        [MenuItem("Are You Fruits?/Script Tag Searcher")]
        private static void Create()
        {
            EditorWindow.GetWindow<ScriptTagSearcherWindow>("Script Tag Searcher");
        }

        private void OnGUI()
        {
            DrawTypeFilter();
            DrawIncludeAutomaticTags();
            
            if (indexedTypes is null || indexedTags is null)
            {
                lastShouldIncludeAutoTags = shouldIncludeAutoTags;
                (indexedTypes, indexedTags) = IndexSearch();
            }

            if ((shouldIncludeAutoTags != lastShouldIncludeAutoTags) || (typeFilter != lastTypeFilter))
            {
                if (GUILayout.Button("Apply indexing filters"))
                {
                    lastShouldIncludeAutoTags = shouldIncludeAutoTags;
                    lastTypeFilter = typeFilter;
                    (indexedTypes, indexedTags) = IndexSearch();
                    lastTagFilter = null;
                    filteredTypes = null;
                }
            }
            else
            {
                GUILayout.Label(string.Empty);
            }

            DrawTagFilter();
            
            if (tagFilter != lastTagFilter)
            {
                lastTagFilter = tagFilter;
                filteredTags = indexedTypes.Keys.Where(k => k.Contains(tagFilter)).ToArray();
            }
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("SelectedTags");
            if (selectedTags.Any())
            {
                if (GUILayout.Button("Clear"))
                {
                    selectedTags.Clear();
                }
            }

            EditorGUILayout.EndHorizontal();

            var wasGuiEnabled = GUI.enabled;
            GUI.enabled = false;

            // var style = new GUIStyle(EditorStyles.textArea);
            // style.wordWrap = true;
            EditorGUILayout.TextArea(string.Join(", ", selectedTags));

            GUI.enabled = wasGuiEnabled;

            EditorGUILayout.BeginHorizontal();
            DrawTags(out var areSelectedTagsChanged);

            if (areSelectedTagsChanged || filteredTypes is null)
            {
                filteredTypes = FilterTypes(selectedTags);
            }

            DrawScripts();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawIncludeAutomaticTags()
        {
            shouldIncludeAutoTags = EditorGUILayout.Toggle("Include automatic tags", EditorPrefs.GetBool(ShouldIncludeAutoTagsKey));
            EditorPrefs.SetBool(ShouldIncludeAutoTagsKey, shouldIncludeAutoTags);
        }
        
        private void DrawTagFilter()
        {
            tagFilter = EditorGUILayout.TextField("Tag filter", EditorPrefs.GetString(TagFilterKey));
            EditorPrefs.SetString(TagFilterKey, tagFilter);
        }

        private void DrawTypeFilter()
        {
            typeFilter = EditorGUILayout.TextField("Type filter", EditorPrefs.GetString(TypeFilterKey));
            EditorPrefs.SetString(TypeFilterKey, typeFilter);
        }

        private void DrawTags(out bool areSelectedTagsChanged)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(100));
            
            EditorGUILayout.LabelField("Tags", EditorStyles.boldLabel);

            areSelectedTagsChanged = false;

            if (filteredTags?.Length is > MaxTagsCount)
            {
                GUILayout.Label("Too many tag results.");
                EditorGUILayout.EndVertical();
                return;
            }
            
            var selectedTagStyle = new GUIStyle(GUI.skin.button);
            selectedTagStyle.normal.textColor = Color.green;
            selectedTagStyle.active.textColor = Color.green;
            selectedTagStyle.focused.textColor = Color.green;
            selectedTagStyle.hover.textColor = Color.green;
            
            tagsScrollPosition = EditorGUILayout.BeginScrollView(tagsScrollPosition);
            foreach (var filteredTag in filteredTags)
            {
                var style = selectedTags.Contains(filteredTag) switch
                {
                    true => selectedTagStyle,
                    false => GUI.skin.button,
                };

                if (GUILayout.Button(filteredTag, style))
                {
                    areSelectedTagsChanged = true;
                    
                    if (!selectedTags.Add(filteredTag))
                    {
                        selectedTags.Remove(filteredTag);
                    }
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void DrawScripts()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Types", EditorStyles.boldLabel);

            if (filteredTypes.Length > MaxTypesCount)
            {
                GUILayout.Label("Too many type results.");
                EditorGUILayout.EndVertical();
                return;
            }

            var scriptStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleLeft,
            };

            typesScrollPosition = EditorGUILayout.BeginScrollView(typesScrollPosition);
            foreach (var taggedType in filteredTypes)
            {
                if (GUILayout.Button(taggedType.FullName, scriptStyle))
                {
                    var scriptFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);

                    string selectedScriptFile = null;
                    
                    foreach (var scriptFile in scriptFiles)
                    {
                        var scriptBody = File.ReadAllText(scriptFile);
                        
                        if (scriptBody.Contains($"class {taggedType.Name}")
                         || scriptBody.Contains($"struct {taggedType.Name}")
                         || scriptBody.Contains($"enum {taggedType.Name}")
                         || scriptBody.Contains($"delegate {taggedType.Name}")
                         || scriptBody.Contains($"interface {taggedType.Name}"))
                        {
                            selectedScriptFile = scriptFile;
                            break;
                        }
                    }

                    if (selectedScriptFile is not null)
                    {
                        AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(GetProjectRelatedPath(selectedScriptFile)));
                    }
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private static string GetProjectRelatedPath(string path)
        {
            var projectPath = GetProjectFolderPath();

            if (path.StartsWith(projectPath))
            {
                return path.Substring(projectPath.Length);
            }

            throw new ArgumentOutOfRangeException();
        }

        private static string GetProjectFolderPath()
        {
            var path = Application.dataPath;

            if (path.EndsWith("/Assets"))
            {
                path = path.Substring(0, path.Length - "/Assets".Length);
            }

            return path + '/';
        }

        private Type[] FilterTypes(IEnumerable<string> tags)
        {
            if (!tags.Any())
            {
                return indexedTags.Keys.ToArray();
            }

            var types = new HashSet<Type>();

            foreach (var type in indexedTypes[tags.First()])
            {
                types.Add(type);
            }

            foreach (var tag in tags.Skip(1))
            {
                var toRemove = new List<Type>();

                foreach (var type in types)
                {
                    if (!indexedTags[type].Contains(tag))
                    {
                        toRemove.Add(type);
                    }   
                }

                foreach (var type in toRemove)
                {
                    types.Remove(type);
                }
            }
            
            return types.OrderBy(t => t.FullName).ToArray();
        }

        private (Dictionary<string, HashSet<Type>> IndexedTypes, Dictionary<Type, HashSet<string>> IndexedTags) IndexSearch()
        {
            var indexedTypes = new Dictionary<string, HashSet<Type>>();
            var indexedTags = new Dictionary<Type, HashSet<string>>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.FullName is not { } typeName || !typeName.Contains(typeFilter))
                    {
                        continue;
                    }

                    if (typeName.Contains('<') && typeName.Contains('>'))
                    {
                        continue;
                    }
                    
                    if (type.GetCustomAttribute<ScriptTagAttribute>() is { } attribute)
                    {
                        foreach (var tag in attribute.Tags)
                        {
                            var tagString = tag.ToString();
                            
                            indexedTypes.GetOrInsertNew(tagString).Add(type);
                            indexedTags.GetOrInsertNew(type).Add(tagString);
                        }
                    }

                    if (shouldIncludeAutoTags)
                    {
                        foreach (var tag in DivideWordsByCapitalLetters(typeName))
                        {
                            indexedTypes.GetOrInsertNew(tag).Add(type);
                            indexedTags.GetOrInsertNew(type).Add(tag);   
                        }
                    }
                }
            }

            return (indexedTypes, indexedTags);
        }

        private static string[] DivideWordsByCapitalLetters(string text)
        {
            var results = new List<string>();

            StringBuilder wordBuilder = null;

            foreach (var c in text)
            {
                if (c is >= 'a' and <= 'z')
                {
                    wordBuilder ??= new();
                    wordBuilder.Append(c);
                }
                else if (c is >= 'A' and <= 'Z')
                {
                    if (wordBuilder is not null)
                    {
                        results.Add(wordBuilder.ToString());
                    }

                    wordBuilder = new();
                    wordBuilder.Append(c);
                }
                else
                {
                    if (wordBuilder is not null)
                    {
                        results.Add(wordBuilder.ToString());

                        wordBuilder = null;
                    }
                }
            }
            
            if (wordBuilder is not null)
            {
                results.Add(wordBuilder.ToString());
            }

            return results.ToArray();
        }
    }
}

#endif