using UnityEngine;

namespace FPSGame
{
    // 적 캐릭터의 애니메이션 관리를 담당하는 스크립트.
    public class EnemyAnimationController : MonoBehaviour
    {
        // Animator 컴포넌트 참조 변수.
        [SerializeField] private Animator refAnimator;

        // 적 캐릭터가 상태 변경을 알리면(이벤트를 통해) 실행할 메소드.
        // 리스너(Listener) 메소드.
        public void OnEnemyStateChanged(EnemyStateManager.State state)
        {
            refAnimator.SetInteger("State", (int)state);
        }

        // 이벤트 리스너 메소드.
        public void OnFire()
        {
            refAnimator.SetTrigger("Fire");
        }

        public void OnReload()
        {
            refAnimator.SetTrigger("Reload");
        }
    }
}