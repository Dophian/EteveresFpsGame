using UnityEngine;

namespace FPSGame
{
    public class PlayerAnimationController : MonoBehaviour
    {
        // Animator 컴포넌트.
        [SerializeField] private Animator animator;

        // 상하 조준 애니메이션 설정에 사용할 변수.
        [SerializeField] private CameraRig cameraRig;
        [SerializeField] private float rotationOffset = 0.5f;

        private void Update()
        {
            // AimAngle 파라미터에 값 설정.
            animator.SetFloat("AimAngle", cameraRig.GetXRotation() * rotationOffset);
        }

        // 재장전 애니메이션 함수.

        public void OnReload()
        {
            // Reload 트리거 파라미터를 설정.
            animator.SetTrigger("Reload");
        }

        // 재장전 애니메이션이 완료될 때까지 걸리는 시간을 계산하는 함수.
        public float WaitTimeToRelaod()
        {
            // 세 번째 레이어(=Reload, Index:2)에서 재생되고 있는
            // 애니메이션 길이 / 재생 속조(배수).
            return animator.GetCurrentAnimatorStateInfo(2).length / animator.GetFloat
                ("ReloadSpeed");
        }

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