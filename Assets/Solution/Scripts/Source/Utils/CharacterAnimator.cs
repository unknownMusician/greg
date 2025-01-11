using UnityEngine;

namespace Greg.Utils
{
    public sealed class CharacterAnimator : MonoBehaviour
    {
        private static readonly int AnimatorBoolIsMoving = Animator.StringToHash("IsMoving");
        
        [SerializeField] private Animator animator;
        [SerializeField] private float threshold;
        
        private Vector3 lastPosition;
        
        private void Update()
        {
            // todo: to positions or smth
            var isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
                           Input.GetKey(KeyCode.D);
            
            animator.SetBool(AnimatorBoolIsMoving, isMoving);

            lastPosition = transform.position;
        }
    }
}