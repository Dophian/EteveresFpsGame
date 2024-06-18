using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace FPSGame
{
    // 상태 기계를 관리할 관리자 스크립트.
    public class EnemyStateManager : MonoBehaviour
    {
        // 적 캐릭터가 가질 상태를 나타내는 열거형.
        public enum State
        {
            Idle,
            Patrol,
            Trace,
            Attack,
            Dead,
            None
        }

        // 적 캐릭터의 현재 상태 값을 나타냄.
        [SerializeField] private State state = State.None;

        // 각 상태를 담당할 스크립트 클래스의 배열 (에디터에서 설정).
        [SerializeField] private EnemyState[] states;

        // 적 캐릭터 데이터 변수.
        [SerializeField] private EnemyData data;

        // 적 캐릭터의 상태가 변경되면 발행되는 이벤트.
        [SerializeField] private UnityEvent<State> OnEnemyStateChanged;

        // 내비 메시 에이전트 프로퍼티.
        public NavMeshAgent Agent { get; private set; }

        // 플레이어 정보.
        public Transform PlayerTransform { get; private set; }

        // 플레이어의 생존 여부.
        public bool IsPlayerDead { get; private set; }

        private void Awake()
        {
            // 내비 메시 에이전트 초기화.
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.isStopped = true;

            // 데이터 초기화.
            // Waypoint 게임 오브젝트 검색 후 트랜스폼 전달.
            data.Initialize(GameObject.FindGameObjectWithTag("WaypointGroup").transform);

            // 상태를 순회하면서 데이터 설정.
            foreach (var state in states)
            {
                state.SetData(data);
            }

            // 플레이어 정보 초기화.
            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            // 처음 시작할 때 Idle 상태로 시작.
            SetState(State.Idle);
        }

        // 상태 전환 메소드 (메시지).
        public void SetState(State newState)
        {
            // 예외처리 (변경하려는 상태가 현재 상태와 같은지 확인).
            if (state == newState || state == State.Dead)
            {
                return;
            }

            // 기존 상태 비활성화.
            if (state != State.None)
            {
                states[(int)state].enabled = false;
            }

            // 새로운 상태 활성화.
            if (newState != State.None)
            {
                states[(int)newState].enabled = true;
            }

            // 상태 변수 업데이트.
            state = newState;

            // 상태 변경 이벤트 발행.
            OnEnemyStateChanged?.Invoke(state);
        }

        // 내비 메시 에이전트를 이동시킬 때 사용할 메소드.
        public void SetAgentDestination(Vector3 destination, float moveSpeed)
        {
            Agent.SetDestination(destination);
            Agent.speed = moveSpeed;
            Agent.isStopped = false;
            Agent.updateRotation = true;
        }

        // 에이전트를 정지시키는 메세지.
        public void StopAgent()
        {
            Agent.isStopped = true;
            Agent.velocity = Vector3.zero;
            Agent.updateRotation = false;
        }

        // 죽었을 때 실행될 메세지(공개 메소드).
        public void OnEnemyDead()
        {
            // 상태를 Dead로 변경.
            SetState(State.Dead);

            // 내미 베시 에이전트 정지.
            StopAgent();
        }

        // 적 캐릭터의 죽음 여부.
        public bool IsDead
        {
            get { return state == State.Dead;}
        }

        // 플레이어가 죽었을 때 실행될 이벤트 리스너 메소드.
        public void OnPlayerDead()
        {
            IsPlayerDead = true;
            SetState(State.Idle);
            StopAgent();
        }
    }
}