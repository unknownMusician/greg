#if UNITY_EDITOR

using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Greg.Meta.Windows
{
    public sealed class HandlersResourcesUsageWindow : EditorWindow
    {
        private (Type Handler, Type[] Resources)[] handlersWithResources;
        private bool[] foldouts;
        private Vector2 resourcesByHandlerScroll;
        private int snapshot = -1;
        
        [MenuItem("Are You Fruits?/Handlers Resources Usage")]
        private static void Create()
        {
            EditorWindow.GetWindow<HandlersResourcesUsageWindow>("Handlers Resources Usage");
        }

        private void OnGUI()
        {
            if (snapshot != HandlersOrderingDeterminismInfoHolder.Snapshot)
            {
                snapshot = HandlersOrderingDeterminismInfoHolder.Snapshot;
                var handlers = HandlersOrderingDeterminismInfoHolder.Handlers.ToArray();
                
                SortTypesByName(handlers);

                handlersWithResources = handlers.Select(h =>
                {
                    var resourcesByHandler = HandlersOrderingDeterminismInfoHolder.GetResourcesByHandler(h).ToArray();
                    
                    SortTypesByName(resourcesByHandler);
                    
                    return (h, resourcesByHandler);
                }).ToArray();

                Array.Resize(ref foldouts, handlers.Length);
            }

            EditorGUILayout.LabelField("Resources by Handler", EditorStyles.boldLabel);
            
            resourcesByHandlerScroll = EditorGUILayout.BeginScrollView(resourcesByHandlerScroll);
            
            for (var i = 0; i < handlersWithResources.Length; i++)
            {
                var (handler, resources) = handlersWithResources[i];
                foldouts[i] = EditorGUILayout.Foldout(foldouts[i], handler.Name);
                
                if (foldouts[i])
                {
                    EditorGUI.indentLevel++;
                    
                    foreach (var resource in resources)
                    {
                        EditorGUILayout.LabelField(resource.Name);
                    }
                    
                    EditorGUI.indentLevel--;
                }
            }
            
            EditorGUILayout.EndScrollView();
        }

        private void SortTypesByName(Type[] types)
        {
            Array.Sort(types, (t0, t1) => t0.Name.CompareTo(t1.Name));
        }
    }
}
#endif