using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FPSGame
{
    // 플레이어의 HP관련 UI 처리를 담당하는 스크립트.
    public class PlayerHPUIController : MonoBehaviour
    {
        // 피격 효과를 위한 UI 참조 변수.
        [SerializeField] private Image bloodEffect;
        private WaitForSeconds effectWait;

        // HP 게이지를 보여줄 UI 이미지 변수.
        [SerializeField] private Image hpBar;

        private void OnEnable()
        {
            if (effectWait == null)
            {
                effectWait = new WaitForSeconds(0.2f);
            }
        }

        // HP 수치가 변경되면 호출될 이벤트 리스너 메소드.
        public void OnPlayerHPChanged(float currentHP, float maxHP)
        {
            hpBar.fillAmount = currentHP / maxHP;
        }

        // 피격 효과 재새 메소드.
        public void PlayBloodEffect()
        {
            StartCoroutine(ShowBloodEffect());
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