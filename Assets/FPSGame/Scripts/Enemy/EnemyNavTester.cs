using UnityEngine;
using UnityEngine.AI;

namespace FPSGame
{
    // 네비게이션 메시 에이전트 테스트 스크립트.
    public class EnemyNavTester : MonoBehaviour
    {
        // 길찾기 시스템 위에서 이동을 할 수 있는 에이전트 클래스.
        [SerializeField] private NavMeshAgent agent;

        // 이동할 목적지.
        [SerializeField] private Transform destination;

        // 트랜스폼 컴포넌트 참조 변수.
        private Transform refTransform;

        private void Awake()
        {
            // 에이전트에 목적지 설정.
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }

            if (refTransform == null)
            {
                refTransform = transform;
            }

            agent.SetDestination(destination.position);
            agent.isStopped = false;
        }

        private void Update()
        {
            // 도착했는지 확인 후 도착했으면 에이전트 정지.
            // 1. 도착했는지 확인 → 목표위치와 내 위치와의 거리를 측정해서 확인.
            // 피타고라스 정리를 총해서 벡터의 거리를 계산해줌.
            //float remainDistance
            //    = (destination.position - refTransform.position).magnitude;
            float remainDistance
                = (destination.position - refTransform.position).sqrMagnitude;

            // 2. 정지.
            //if (remainDistance <= 0.2f)
            if (remainDistance <= (0.2f * 0.2f))
            {
                // 정지 처리.
                agent.isStopped = true;

                // 속력 0으로 설정.
                agent.velocity = Vector3.zero;
            }
        }
    }
}