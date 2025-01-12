using AreYouFruits.MonoBehaviourUtils.Unity;
using UnityEngine;

namespace Greg.Utils
{
    public sealed class ChildsVisualizer : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
                
            foreach (var child in transform.GetChildren())
            {
                Gizmos.DrawSphere(child.position, 10);
            }
        }
    }
}