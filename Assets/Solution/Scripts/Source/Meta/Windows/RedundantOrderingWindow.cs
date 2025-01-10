#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Greg.Meta.Windows
{
    public sealed class RedundantOrderingWindow : EditorWindow
    {
        private HashSet<(Type, Type)> redundantOrderings;
        private int snapshot = -1;
        private Vector2 redundantOrderingsScroll;
        
        [MenuItem("Are You Fruits?/Redundant Ordering")]
        private static void Create()
        {
            EditorWindow.GetWindow<RedundantOrderingWindow>("Redundant Ordering");
        }
        
        private void OnGUI()
        {
            if (snapshot != HandlersOrderingDeterminismInfoHolder.Snapshot)
            {
                snapshot = HandlersOrderingDeterminismInfoHolder.Snapshot;
                
                var neededOrderings = Utils.GetNeededOrderings();
                redundantOrderings = Utils.GetRedundantOrderings(neededOrderings);
            }
            
            if (!redundantOrderings.Any())
            {
                EditorGUILayout.LabelField("Redundant orderings: None", EditorStyles.boldLabel);
                return;
            }

            var redundantOrderingsLabel = new GUIStyle(EditorStyles.boldLabel);
            redundantOrderingsLabel.normal.textColor = Color.yellow;

            EditorGUILayout.LabelField("Redundant orderings", redundantOrderingsLabel);
            redundantOrderingsScroll = EditorGUILayout.BeginScrollView(redundantOrderingsScroll);
            EditorGUI.indentLevel++;

            foreach (var (i, (handler0, handler1)) in redundantOrderings.Select((x, i) => (i, x)))
            {
                var controlRect = EditorGUILayout.GetControlRect(hasLabel: false, height: EditorGUIUtility.singleLineHeight, GUILayout.ExpandWidth(true));

                var color = (i % 2 is 0) switch
                {
                    true => new Color(0.25f, 0.25f, 0.25f),
                    false => new Color(0.2f, 0.2f, 0.2f),
                };

                EditorGUI.DrawRect(controlRect, color);

                EditorGUI.LabelField(controlRect, $"{handler0.Name} - {handler1.Name}");
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif