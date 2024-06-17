﻿using UnityEngine;
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
            None
        }

        // 적 캐릭터의 현재 상태 값을 나타냄.
        [SerializeField] private State state = State.None;

        // 각 상태를 담당할 스크립트 클래스의 배열 (에디터에서 설정).
        [SerializeField] private EnemyState[] states;

        // 적 캐릭터 데이터 변수.
        [SerializeField] private EnrmyData data;

        // 적 캐릭터의 상태가 변경되면 발행되는 이벤트.
        [SerializeField] private UnityEvent<State> OnEnemyStateChanged;

        // 내비 메시 에이전트 프로퍼티.
        public NavMeshAgent Agent { get; private set; }

        private void Awake()
        {
            // 내비 메시 에이전트 초기화.
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.isStopped = true;

            // 데이터 초기화.
            // Waypoint 게임 오브젝트 검색 후 트랜스폼 전달.
            data.Initialize(GameObject.FindGameObjectWithTag("WaypointGroup").transform);

            // 상태를 ㅅ누회하면서 데이터 설정.
            foreach (var state in states)
            {
                state.SetData(data);
            }

            // 처음 시작할 때 Idle 상태로 시작.
            SetState(State.Idle);
        }

        // 상태 전환 메소드 (메시지).
        public void SetState(State newState)
        {
            // 예외처리 (변경하려는 상태가 현재 상태와 같은지 확인).
            if (state == newState)
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
            Agent.speed = data.PatrolSpeed;
            Agent.isStopped = false;
            Agent.updateRotation = true;
        }
    }
}