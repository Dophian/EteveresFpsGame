using UnityEngine;

namespace FPSGame
{

    // 사용자의 무기를 관리하는 스크립트.
    public class PlayerWeaponController : MonoBehaviour
    {
        // 무기를 장착할 때 사용할 뼈대 위치 (트랜스폼).
        [SerializeField] private Transform weaponHolder;

        // 장착할 무기.
        [SerializeField] private PlayerWeapon weapon;

        private void Awake()
        {
            // 무기 장착.
            weapon.LoadWeapon(weaponHolder);
        }

        private void Update()
        {
            // 입력 확인 후 발사.
            if (PlayerInputManager.IsFire)
            {
                // 발사 명령 전달.
                weapon.Fire();
            }
        }
    }
}