using UnityEngine;

namespace FPSGame
{
    // 적 캐릭터가 죽었을 때 실행될 상태 스크립트.
    public class EnemyDeadState : EnemyState
    {
        // 정리 및 제거할 필드.
        [SerializeField] private Collider enemyCollider;

        // 제거할 게임 오브젝트.
        [SerializeField] private GameObject destroyTarget;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 에이전트 정지.
            //manager.StopAgent();

            // 콜라이더 비활성화.
            enemyCollider.enabled = false;

            // 게임 오브젝트 삭제 대기 신청.
            Destroy(destroyTarget, 5f);

            // 태그 제거.
            refTransform.tag = "Untagged";
        }
    }
}