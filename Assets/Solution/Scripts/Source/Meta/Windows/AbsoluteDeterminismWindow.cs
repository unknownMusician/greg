#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Greg.Meta.Windows
{
    public sealed class AbsoluteDeterminismWindow : EditorWindow
    {
        private Dictionary<(Type, Type), HashSet<Type>> missingOrderings;
        private int snapshot = -1;
        private Vector2 missingOrderingsScroll;
        
        [MenuItem("Are You Fruits?/Absolute Determinism")]
        private static void Create()
        {
            EditorWindow.GetWindow<AbsoluteDeterminismWindow>("Absolute Determinism");
        }
        
        private void OnGUI()
        {
            if (snapshot != HandlersOrderingDeterminismInfoHolder.Snapshot)
            {
                snapshot = HandlersOrderingDeterminismInfoHolder.Snapshot;
                
                var neededOrderings = Utils.GetNeededOrderings();
                missingOrderings = Utils.GetMissingAbsoluteOrderings(neededOrderings);
            }

            if (!missingOrderings.Any())
            {
                EditorGUILayout.LabelField("Missing orderings: None", EditorStyles.boldLabel);
            }
            else
            {
                var missingOrderingsLabel = new GUIStyle(EditorStyles.boldLabel);
                missingOrderingsLabel.normal.textColor = Color.red;

                EditorGUILayout.LabelField("Missing orderings", missingOrderingsLabel);
                missingOrderingsScroll = EditorGUILayout.BeginScrollView(missingOrderingsScroll);
                EditorGUI.indentLevel++;

                foreach (var (i, ((handler0, handler1), resources)) in missingOrderings.Select((x, i) => (i, x)))
                {
                    var controlRect = EditorGUILayout.GetControlRect(hasLabel: false,
                        height: EditorGUIUtility.singleLineHeight, GUILayout.ExpandWidth(true));

                    var color = (i % 2 is 0) switch
                    {
                        true => new Color(0.25f, 0.25f, 0.25f),
                        false => new Color(0.2f, 0.2f, 0.2f),
                    };

                    EditorGUI.DrawRect(controlRect, color);

                    var resourcesAsString = string.Join(", ", resources.Select(static r => r.Name));

                    EditorGUI.LabelField(controlRect, $"({resourcesAsString}) {handler0.Name} - {handler1.Name}");
                }

                EditorGUI.indentLevel--;
                EditorGUILayout.EndScrollView();
            }
        }
    }
}

#endif