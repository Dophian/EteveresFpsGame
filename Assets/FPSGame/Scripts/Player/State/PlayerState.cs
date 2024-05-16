using UnityEngine;

namespace FPSGame
{
    // 플레이어 상태(스테이트)의 기반 클래스.
    // 상태의 진입점-업데이트-종료 메소드 및 공통 기능 제공.
    public class PlayerState : MonoBehaviour
    {
        // 트랜스폼 참조 변수.
        protected Transform refTransform;

        // 캐릭터 컨트롤러 컴포넌트 참조 변수.
        [SerializeField] private CharacterController characterController;

        // 상태 진입 함수.
        protected virtual void OnEnable()
        {
            // 트랜스폼 컴포넌트 초기화.
            if (refTransform == null)
            {
                refTransform = transform;
            }

            // 컴포넌트 초기화.
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
        }

        // 상태 업데이트 함수.
        protected virtual void Update()
        {

        }

        // 상태 종료 함수.
        protected virtual void OnDisable()
        {

        }
    }
}