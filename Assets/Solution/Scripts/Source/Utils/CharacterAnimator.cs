using UnityEngine;

namespace Greg.Utils
{
    public sealed class CharacterAnimator : MonoBehaviour
    {
        private static readonly int AnimatorBoolIsMoving = Animator.StringToHash("IsMoving");
        
        [SerializeField] private Animator animator;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private float threshold;
        
        private Vector3 lastPosition;
        
        private void Update()
        {
            var isMoving = rigidbody.linearVelocity.sqrMagnitude > threshold * threshold;
            
            animator.SetBool(AnimatorBoolIsMoving, isMoving);

            lastPosition = transform.position;
        }
    }
}