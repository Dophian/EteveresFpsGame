using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSGame
{
    // 적 캐릭터가 공격 상태일 때 실행되는 스크립트.
    // 발사가 가능한 지 확인 후 발사를 진행.
    // 플레이어와의 위치를 계산해서 다시 쫓아가거나 정찰로 상태 전환.
    public class EnemyAttackState : EnemyState
    {
        // 필드.
        // 이벤트.
        // 탄약 발사 이벤트.
        [SerializeField] private UnityEvent OnFire;

        // 재장전 이벤트.
        [SerializeField] private UnityEvent OnReload;

        // 변수들.
        // 발사 가능 시간 계산에 필요한 변수.
        private float nextFireTime = 0f;

        // 탄약 계산에 필요한 변수.
        private int currentBullet = 0;

        // 기타.
        private bool isReload = false;                  // 재장전 중인지 여부.
        private WaitForSeconds waitForReload = null;    // 코루틴에서 반복해서 쓸 객체.

        protected override void OnEnable()
        {
            base.OnEnable();

            // 데이터 초기화.
            if (currentBullet == 0)
            {
                currentBullet = data.MaxBullet;
            }

            if (waitForReload == null)
            {
                waitForReload = new WaitForSeconds(data.ReloadTime);
            }

            // 쏠 때는 정지.
            manager.StopAgent();

            // 다음에 발사할 시간 계산.
            // Time.time은 현재 시간을 반환해줌.
            nextFireTime = Time.time + data.FireRate + Random.Range(0.1f, 0.3f);

            // 플레이어가 죽었는지 확인 후, 죽었으면 대기 상태로 전환.
            if (manager.IsPlayerDead)
            {
                manager.SetState(EnemyStateManager.State.Idle);
            }

        }

        protected override void Update()
        {
            base.Update();

            // 재장전인지 확인.
            if (isReload)
            {
                return;
            }

            // 플레이어가 죽었는지 확인 후, 죽었으면 대기 상태로 전환.
            if (manager.IsPlayerDead)
            {
                manager.SetState(EnemyStateManager.State.Idle);
                return;
            }
            // 발사 및 관현 데이터 업데이트.
            UpdateFireState();

            // 플레이어를 바라보도록 회전 처리.
            Vector3 directionToPlayer
                = manager.PlayerTransform.position - refTransform.position;
            UpdateRotation(directionToPlayer, data.AttackRotateDamping);

            // 플레이어와의 위치가 멀어지면, 쫓아가거나 정찰로 전환.
            float distanceToPlayer =
                (manager.PlayerTransform.position - refTransform.position).magnitude;

            if (distanceToPlayer > data.AttackDistance)
            {
                manager.SetState(EnemyStateManager.State.Trace);
            }
        }

        // 발사 메소드
        private void UpdateFireState()
        {
            // 발사가 가능한 충분한 시간이 지났는지 확인.
            if (Time.time > nextFireTime)
            {
                // 발사 이벤트 발행.
                OnFire?.Invoke();

                // 다음 발사가 가능한 미래의 시간 설정.
                nextFireTime = Time.time + data.FireRate + Random.Range(0.1f, 0.3f);

                // 탄약 수 업데이트.
                --currentBullet;

                // 재장전 여부 확인 후 재장전 처리.
                isReload = currentBullet == 0;

                if (isReload)
                {
                    StartCoroutine(Reload());
                }
            }
        }

        // 재장전 메소드.
        private IEnumerator Reload()
        {
            // 재장전 이벤트 발행 -> 애니메이션에 전달을 위해서.
            OnReload?.Invoke();

            // 재장전 애니메이션이 재생될 충분한 시간동안 대기.
            yield return waitForReload;

            // 데이터 업데이트.
            currentBullet = data.MaxBullet;
            isReload = false;
        }
    }
}