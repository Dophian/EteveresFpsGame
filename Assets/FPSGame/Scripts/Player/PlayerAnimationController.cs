using UnityEngine;

namespace FPSGame
{
    public class PlayerAnimationController : MonoBehaviour
    {
        // Animator 컴포넌트.
        [SerializeField] private Animator animator;

        // State 값 설정 함수.
        public void SetStateParameter(int state)
        {
            animator.SetInteger("State", state);
        }

        // Animator에서 Horizontal 파라미터를 설정하는 함수.
        public void SetHorizontalParameter(float horizontal)
        {
            // 값 정리.
            horizontal = horizontal > 0f ? 1f : horizontal < 0f ? -1f : 0f;

            // 파라미터 설정
            animator.SetFloat("Horizontal", horizontal);
        }

        // Animator에서 Vertical 파라미터를 설정하는 함수.
        public void SetVerticalParameter(float vertical)
        {
            vertical = vertical > 0f ? 1f : vertical < 0f ? -1f : 0f;
            
            // 파라미터 설정
            animator.SetFloat("Vertical", vertical);
        }
    }
}