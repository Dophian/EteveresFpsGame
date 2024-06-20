using UnityEngine;
using UnityEngine.Events;

namespace FPSGame
{
    // 적 캐릭터의 대미지 처리를 담당하는 스크립트.
    public class EnemyDamageController : MonoBehaviour
    {
        // 총을 맞았을 때 재생할 이펙트 프리팹.
        [SerializeField] private GameObject bloodEffect;

        // 적 캐릭터 데이터 컴포넌트.
        [SerializeField] private EnemyData data;

        // 적 캐릭터가 죽을 때 발행하는 이벤트.
        [SerializeField] private UnityEvent OnEnemyDead;

        // 적 캐릭터가 대미지를 입어서 체력이 변경될 때 발행하는 이벤트.
        [SerializeField] private UnityEvent<float, float> OnEnemyDamaged;

        // 충돌이 발생했을 때 비교할 탄약 태그 값.
        private const string bulletTag = "Bullet";

        // 체력.
        private float hp = 0f;

        private void OnEnable()
        {
            // 체력 값 설정.
            hp = data.MaxHP;

            // 체력 변동 이벤트 발행.
            OnEnemyDamaged?.Invoke(hp, data.MaxHP);
        }

        // 다른 물체와 Collision 방식으로 충돌을 시작할 때
        // 유니티 엔진이 호출해주는 이벤트 함수.
        private void OnCollisionEnter(Collision collision)
        {
            // 충돌한 물체가 탄약인지 확인.
            if (collision.collider.CompareTag(bulletTag))
            {
                // 피격 효과 재생.
                ShowBloodEffect(collision);

                // 탄약 제거.
                Destroy(collision.gameObject);

                // 대미지 처리 (체력 감소).
                hp -= collision.gameObject.GetComponent<BulletDamage>().Damage;
                // 값 정리.
                hp = hp < 0f ? 0f : hp;

                // 체력 변동 이벤트 발행.
                OnEnemyDamaged?.Invoke(hp, data.MaxHP);

                // 죽음 판단.
                if (hp == 0f)
                {
                    // 죽었다는 이벤트 발행.
                    OnEnemyDead?.Invoke();

                    // 점수 획득.
                    GameManager.Instance.AddScore();
                    //GameMAnager.Get().AddScor();

                    //if (OnEnemyDead != null)
                    //{
                    //    OnEnemyDead.Invoke();
                    //}
                }
            }
        }

        // 피격 효과 재생 메소드.
        private void ShowBloodEffect(Collision collision)
        {
            // 맞은 위치 지점 구하기.
            Vector3 position = collision.contacts[0].point;

            // 맞은 위치의 노멀(법선) 구하기.
            Vector3 normal = collision.contacts[0].normal;

            // 노멀을 기준으로 적절한 회전 구하기.
            Quaternion rotation = Quaternion.LookRotation(normal);

            // 위치/회전을 사용해서 프리팹 생성.
            GameObject effect = Instantiate(bloodEffect, position, rotation);

            // 재생 후 프리팹 제거.
            Destroy(effect, 1f);
        }
    }
}