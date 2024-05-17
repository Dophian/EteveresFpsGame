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
        [SerializeField] protected CharacterController characterController;

        // 회전 속도.
        //[SerializeField] protected float rotationSpeed = 540f;

        // 플레이어 데이터 ScriptableObject.
        protected PlayerData data;

        // 플레이어 데이터를 설정하는 함수.

        public void SetData(PlayerData data)
        {
            // this 키워드는 나자신을 가리킴.
            this.data = data;
        }

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
            // 캐릭터의 중력 적용.
            Vector3 gravity = new Vector3(0f, -9.8f, 0f);
            characterController.Move(gravity * Time.deltaTime);

            // 좌우 캐릭터 회전 처리.
            Vector3 rotation = new Vector3(
                0f,
                PlayerInputManager.Turn * data.rotationSpeed * Time.deltaTime,
                0f
            );

            // 회전 적용.
            refTransform.Rotate(rotation);
        }

        // 상태 종료 함수.
        protected virtual void OnDisable()
        {

        }
    }
}