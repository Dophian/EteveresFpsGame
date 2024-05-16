using UnityEngine;

namespace FPSGame
{
    public class AutoDestroy : MonoBehaviour
    {
        // 삭제할 때 까지 대기하는 시간 값 (단위: 초).
        [SerializeField] private float destriyTime = 3f;

        private void Awake()
        {
            // gameObject → 이 컴포넌트가 부착된 게임 오브젝트.
            Destroy(gameObject, destriyTime);
        }
    }
}
