namespace FPSGame
{
    // 적 캐릭터가 플레이어를 쫒아갈 때 사용하는 상태 스크립트.
    public class EnemyTraceState : EnemyState
    {
        // 플레이어 정보 -> 상태 관리자에서 받아옴.
        // 플레이어의 위치.
        // 플레이어가 살아있는 지 여부.

        protected override void OnEnable()
        {
            base.OnEnable();

            // 플레이어의 위치 업데이트.
            UpdatePlayerPosition();

            // Todo: 플레이어가 살아 있는지 확인 후 죽었으면 정찰로 바로 전환.
            if (manager.IsPlayerDead)
            {
                manager.SetState(EnemyStateManager.State.Idle);
                
            }
        }

        protected override void Update()
        {
            base.Update();

            // 플레이어가 살아 있는지 확인 후 죽었으면 정찰로 바로 전환.
            if (manager.IsPlayerDead)
            {
                manager.SetState(EnemyStateManager.State.Idle);
                return;
            }

            // 플레이어의 위치 업데이트 및 따라가기.
            UpdatePlayerPosition();

            // 공격 가능 범위에 접근했으면, 공격 상태로 전환.
            if (manager.Agent.remainingDistance <= data.AttackDistance)
            {
                manager.SetState(EnemyStateManager.State.Attack);
            }
        }

        // 플레이어의 위치를 가져와 NavMeshAgent의 목적지로 설정하는 메소드.
        private void UpdatePlayerPosition()
        {
            manager.SetAgentDestination(
                manager.PlayerTransform.position,
                data.TraceSpeed
            );
        }
    }
}