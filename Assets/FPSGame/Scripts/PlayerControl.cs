using UnityEngine;

namespace FPSGame
{
    public class PlayerControl : MonoBehaviour

    {

        // 열거형.

        public enum State
        {
            Idle,
            Move
        }

        // 플레이어의 상태를 나타내는 변수.
        [SerializeField] private State currentState = State.Idle;

        // 이동 속도.
        [SerializeField] private float moveSpeed = 5f;

        // Animator 컴포넌트 변수.
        [SerializeField] private Animator refAnimator;

        // 트랜스폼 컴포넌트 참조 변수.
        private Transform refTransform;

        private void Awake()
        {
            // 트랜스폼 참조 저장.
            refTransform = transform;

            // 참조 저장.
            refAnimator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            // 방향키 입력 받기.
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // 방향에 대한 애니메이션 설정.
            refAnimator.SetFloat("Horizontal", horizontal > 0f ? 1f : horizontal < 0f ? -1f : 0f);
            refAnimator.SetFloat("Vertical", vertical > 0f ? 1f : vertical < 0f ? -1f : 0f);

            // 애니메이션 설정.
            if (horizontal == 0f && vertical == 0f)
            {
                // 상태 설정.
                currentState = State.Idle;


                // 입력이 없음.
                refAnimator.SetInteger("State", (int)currentState);
            }
            else
            {
                // 상태 설정.
                currentState = State.Move;

                // 입력이 있음.
                refAnimator.SetInteger("State", (int)currentState);
            }

            // 이동.
            refTransform.position +=
                new Vector3(horizontal, 0f, vertical).normalized * moveSpeed * Time.deltaTime;
        }
    }
}