using UnityEngine;

namespace FPSGame
{
    public class PlayerWeapon : MonoBehaviour
    {
        // 오프셋 변수 추가.
        // 위치 및 회전의 조정이 필요할 때 사용할 변수.
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Vector3 rotationOffset;

        protected virtual void Awake()
        {
        }

        // WeaponHolder에 무기를 장착하는 함수.
        // Armed / Equipped / Load.
        public void LoadWeapon(Transform weaponHolder)
        {
            // WeaponHolder를 무기의 부모로 지정.
            transform.SetParent(weaponHolder);

            // 트랜스폼 초기화.
            transform.localPosition = Vector3.zero + positionOffset;
            transform.localRotation = Quaternion.identity * Quaternion.Euler(rotationOffset);
            //transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;
        }

        // 무기에서 탄약을 발사할 때 사용할 함수.
        public virtual void Fire()
        {
        }
    }
}