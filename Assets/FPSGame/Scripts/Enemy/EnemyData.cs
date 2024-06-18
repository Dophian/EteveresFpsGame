using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    // 적 캐릭터가 사용할 데이터 스크립트.
    public class EnemyData : MonoBehaviour
    {
        // 변수.
        [SerializeField] private float attackDistance = 5f;           // 공격 가능 거리(단위 미터).
        public float AttackDistance { get { return attackDistance; } }

        [SerializeField] private float traceDistance = 10f;           // 추격 시작 거리(단위 미터).
        public float TraceDistance { get { return traceDistance; } }

        [SerializeField] private float patrolWaitTime = 3f;           // 정찰할 때까지 대기하는 시간(초).
        public float PatrolWaitTime { get { return patrolWaitTime; } }

        [SerializeField] private float moveRotateDamping = 7f;        // 이동 시 회전 지연 값.
        public float MoveRotateDamping { get { return moveRotateDamping; } }

        [SerializeField] private float attackRotateDamping = 10f;     // 공격 시 회전 지연 값.
        public float AttackRotateDamping { get { return attackRotateDamping; } }

        [SerializeField] private float fireRate = 0.2f;               // 발사 간격(초).
        public float FireRate { get { return fireRate; } }

        [SerializeField] private int maxBullet = 3;                   // 최대 탄약 수.
        public int MaxBullet { get { return maxBullet; } }

        [SerializeField] private float maxHP = 100f;                  // 체력
        public float MaxHP { get { return maxHP; } }

        [SerializeField] private readonly float patrolSpeed = 1.5f;   // 정찰 시 이동 속도.
        public float PatrolSpeed { get { return patrolSpeed; } }

        [SerializeField] private readonly float traceSpeed = 3.5f;    // 추격 시 이동 속도.
        public float TraceSpeed { get { return traceSpeed; } }

        [SerializeField] private readonly float reloadTime = 2f;      // 재장전 시간.
        public float ReloadTime { get { return reloadTime; } }

        [SerializeField] private List<Transform> waypoints;           // 정찰 위치 (배열).
        public List<Transform> Waypoints { get { return waypoints; } }

        // 정찰 위치 초기화 함수.
        public void Initialize(Transform waypointGroup)
        {
            // waypointGroup 트랜스폼의 자식 트랜스폼을 모두 정찰 지점으로 설정.
            waypointGroup.GetComponentsInChildren(waypoints);

            // 0번 트랜스폼은 자기 자신이기 때문에 제거.
            waypoints.RemoveAt(0);
        }
    }
}
