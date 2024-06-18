using UnityEngine;

namespace FPSGame
{
    // 적 캐릭터의 상태 기계에 사용할 상태 스크립트의 기반 클래스.
    public class EnemyState : MonoBehaviour
    {
        // 필드.
        // 상태 관리자 스크립트 참조 변수.
        protected EnemyStateManager manager;

        // 적 캐릭터 데이터 참조 변수.
        // 누군가 이 값을 설정해줘야 함 → 상태 관리자에서 전달해 줌.
        protected EnemyData data;

        // 트랜스폼 참조 변수.
        protected Transform refTransform;

        // 데이터 설정 함수.
        public void SetData(EnemyData data)
        {
            this.data = data;
        }

        // 스테이트. 진입점 - 업데이트 - 종료.
        protected virtual void OnEnable()
        {
            // 참조 변수 초기화.
            if (manager == null)
            {
                manager = GetComponent<EnemyStateManager>();
            }

            if (refTransform == null)
            {
                refTransform = transform;
            }
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }

        // 적 캐릭터가 공격할 때 사용할 회전 메소드.
        protected void UpdateRotation(Vector3 target, float damping)
        {
            if (target != Vector3.zero)
            {
                // 회전 구하기.
                Quaternion rotation = Quaternion.LookRotation(target);

                // Lerp를 활용해서 회전 적용.
                refTransform.rotation = Quaternion.Slerp(
                    refTransform.rotation,
                    rotation,
                    damping * Time.deltaTime
                );
            }
        }
    }
}