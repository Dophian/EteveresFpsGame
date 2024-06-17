using UnityEngine;

namespace FPSGame
{
    // 적 캐릭터가 Idle 상태일 때 실행될 스크립트.
    // 1. 지정한 시간만큼 대기.
    // 2. 정찰 상태로 전환.
    public class EnemyIdleState : EnemyState
    {
        // 대기할 시간 값 (단위: 초).
        [SerializeField] private float waitTime = 0f;
        
        // 경과시간 계산용 변수.
        [SerializeField] private float elapsedTime = 0f;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 대기할 시간 설정.
            waitTime = Random.Range(data.PatrolWaitTime * 0.8f, data.PatrolWaitTime * 1.2f);

            // 경과 시간 변수 초기화.
            elapsedTime = 0f;
        }

        protected override void Update()
        {
            base.Update();

            // 시간 업데이트.
            elapsedTime += Time.deltaTime;

            // 대기 시간보다 더 지났으면,
            if (elapsedTime > waitTime)
            {
                // 상태 전환 → 상태 관리자를 통해 전환.
                manager.SetState(EnemyStateManager.State.Patrol);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            // 사용한 변수 초기화.
            elapsedTime = 0f;
            waitTime = 0f;
        }
    }
}