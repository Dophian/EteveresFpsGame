using UnityEngine;

namespace FPSGame
{
    // 플레이어의 상태(스테이트)를 제어하는 관리자 스크립트.
    public class PlayerStateManager : MonoBehaviour
    {
        // 플레이어의 상태를 나타내는 열거형 선언.
        public enum State
        {
            Idle,
            Move
        }

        // 상태 변수.
        [SerializeField] private State currentState = State.Idle;

        // 스테이트 컴포넌트 배열 변수.
        [SerializeField] private PlayerState[] states;

        // 애니메이션 컨트롤러 변수.
        [SerializeField] private PlayerAnimationController animationController;

        // 상태 설정 함수.

        public void SetState(State newState)
        {
            // 예외 처리.
            if (currentState == newState)
            {
                return;
            }

            // 현재 상태 스크립트 끄기.
            states[(int)currentState].enabled = false;

            // 새로운 상태 스크립트 켜기.
            states[(int)newState].enabled = true;

            // 상태 변수 업데이트.
            currentState = newState;

            // 애니메이션 설정.
            animationController.SetStateParameter((int)currentState);
        }
        private void Update()
        {
            // 입력이 없는 지 확인.
            if (PlayerInputManager.Horizontal == 0f
                && PlayerInputManager.Vertical == 0f)
            {
                // 입력이 없으면 기본 상태로 전환.
                SetState(State.Idle);
            }
            else
            {
                // 이동 상태로 전환.
                SetState(State.Move);

                // 애니메이션 설정.
                animationController.SetHorizontalParameter(PlayerInputManager.Horizontal);

                animationController.SetVerticalParameter(PlayerInputManager.Vertical);
            }
        }
    }
}