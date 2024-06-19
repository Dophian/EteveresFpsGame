using UnityEngine;

namespace FPSGame
{
    // 플레이어의 탄약 정보를 UI에 표시하는 스크립트.
    public class PlayerAmmoUIController : MonoBehaviour
    {
        // 텍스트 UI.
        [SerializeField] private TMPro.TextMeshProUGUI ammoText;

        // 이벤트 리스너 메소드.
        public void OnAmmoChanged(int currentAmmo, int maxAmmo)
        {
            ammoText.text = $"<color=red>{currentAmmo}</color>/{maxAmmo}";
        }
    }
}
