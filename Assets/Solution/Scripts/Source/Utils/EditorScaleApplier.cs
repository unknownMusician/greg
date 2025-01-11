using AreYouFruits.MonoBehaviourUtils.Unity;
using UnityEngine;

namespace Greg.Utils
{
    public sealed class EditorScaleApplier : MonoBehaviour
    {
        [ContextMenu("Apply scale to children")]
        private void ApplyScaleToChildren()
        {
            var parentScale = transform.localScale;

            foreach (var child in transform.GetChildren())
            {
                child.localScale = new Vector3
                {
                    x = child.localScale.x * parentScale.x,
                    y = child.localScale.y * parentScale.y,
                    z = child.localScale.z * parentScale.z,
                };
                
                child.position = new Vector3
                {
                    x = child.position.x * parentScale.x,
                    y = child.position.y * parentScale.y,
                    z = child.position.z * parentScale.z,
                };
            }
            
            transform.localScale = Vector3.one;
        }
    }
}