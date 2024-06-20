using UnityEngine;
using UnityEngine.UI;

namespace FPSGame
{
    // 적 캐릭터가 머리에 달고 다니는 HUB HPBar 스크립트.
    public class EnemyHPBar : MonoBehaviour
    {
        // 필드.
        [SerializeField] private Image hpBar;


        private Transform cameraTransform;
        private Transform refTransform;

        private void OnEnable()
        {
            // Camera.main 은 메인카메라를 찾아서 전달해줌.
            cameraTransform = Camera.main.transform;
            refTransform = transform;
        }
        // 기능.
        // 빌보드 (카메라를 항상 바라보도록 회전을 설정하는 기능).
        // - 카메라 방향.

        private void Update()
        {
            // 카메라의 뒤방향(forward)과 항상 방향을 맞춤.
            //refTransform.LookAt(-cameraTransform.forward);
            refTransform.rotation = Quaternion.LookRotation(cameraTransform.forward);
        }

        // 체력 게이지 처리.
        // - 현재 체력, 최대 체력.
        // - UI Image 컴포넌트.
        public void OnEnemyDamaged(float currentHP, float maxHP)
        {
            // 체력 게이지 설정.
            hpBar.fillAmount = currentHP / maxHP;
        }
    }
}
