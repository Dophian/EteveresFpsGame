using UnityEngine;

namespace FPSGame
{
    public class PlayerWeaponRifle : PlayerWeapon
    {
        // 탄약 발사를 위한 변수.
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform muzzleTransform;

        // 발사할 때 소리 재생을 위한 변수.
        [SerializeField] private AudioSource audioPlayer;
        [SerializeField] private AudioClip fireSound;
        public override void Fire()
        {
            base.Fire();

            // 탄약 게임 오브젝트 생성.
            Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);

            // 발사 소리 재생.
            audioPlayer.PlayOneShot(fireSound);
        }
    }
}