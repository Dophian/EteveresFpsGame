using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FPSGame
{
    // 플레이어의 대미지를 처리하는 스크립트.
    public class PlayerDamageController : MonoBehaviour
    {
        // 필드
        // 플레이어의 체력.
        [SerializeField] private float currentHP = 0f;

        // 플레이어 데이터 컴포넌트 참조 변수.
        [SerializeField] private PlayerData data;

        // 피격 효과를 위한 UI 참조 변수.
        [SerializeField] private Image bloodEffect;
        private WaitForSeconds effectWait;

        private void OnEnable()
        {
            currentHP = data.maxHP;
            if (effectWait == null)
            {
                effectWait = new WaitForSeconds(0.2f);
            }
        }

        // 트리거(Trigger) 타입의 충돌이 발생할 때 엔진이 실행해주는 이벤트 메소드.
        private void OnTriggerEnter(Collider other)
        {
            // 적 캐릭터가 발사한 탄약인지 확인.
            if (other.CompareTag("EnemyBullet"))
            {
                // 대미지 처리.
                currentHP -= other.GetComponent<BulletDamage>().Damage;
                currentHP = currentHP < 0f ? 0f : currentHP;

                // 탄약 제거.
                Destroy(other.gameObject);
                // 죽음 알리기.
                if (currentHP == 0f)
                {
                    // Todo: 이벤트 발행.
                }

                // 피격 효과 재생 (UI로 피격 효과).
                StartCoroutine(ShowBloodEffect());
            }
        }
        // 피격 효과 재생 메소드(코루틴).
        private IEnumerator ShowBloodEffect()
        {
            // 이미지 색상 설정.
            bloodEffect.color = new Color(
                Random.Range(0.7f, 0.9f),
                0f,
                0f,
                Random.Range(0.2f, 0.4f)
            );

            // 잠깐 대기.
            yield return effectWait;

            // 원래 색상으로 복귀.
            bloodEffect.color = Color.clear;
        }

    }
}
