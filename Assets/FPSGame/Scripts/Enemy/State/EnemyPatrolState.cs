using UnityEngine;

namespace FPSGame
{
    //
    public class EnemyPatrolState : EnemyState
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            // 랜덤으로 정찰 지점 고르기.
            int index = Random.Range(0, data.Waypoints.Count);
            Vector3 destination = data.Waypoints[index].position;

            // 내비 메시 에이전트에 정찰 위치를 이동 목표지점으로 설정.
            manager.SetAgentDestination(destination, data.PatrolSpeed);

        }

        protected override void Update()
        {
            base.Update();

            // 플레이어가 시야 안에 들어오면 쫒아가기.
            // 플레이어가 살아있어야 함.
            float distanceToPlayer
                = (manager.PlayerTransform.position - refTransform.position).magnitude;
            if (manager.IsPlayerDead == false && distanceToPlayer <= data.TraceDistance)
            {
                manager.SetState(EnemyStateManager.State.Trace);
                return;
            }

            // 도착했는지 확인 후 도착했으면, Idle 상태로 전환.
            if (manager.Agent.remainingDistance <= 0.2f)
            {
                manager.SetState(EnemyStateManager.State.Idle);

                manager.Agent.isStopped = true;
                manager.Agent.velocity = Vector3.zero;
            }
        }
    }
}