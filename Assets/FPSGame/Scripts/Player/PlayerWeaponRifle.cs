using UnityEngine;
using UnityEngine.Events;

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

        // 탄피 제거 효과 파티클.
        //[SerializeField] private Transform cartridgeEjectTransform;
        [SerializeField] private ParticleSystem cartridgeEjectEffect;
        [SerializeField] private ParticleSystem muzzleFlashEffect;

        // 카메라 흔들기.
        [SerializeField] private CameraShake cameraShake;

        // 플레이어 데이터.
        [SerializeField] private PlayerData data;

        // 현재 남은 탄약 수.
        [SerializeField] private int currentAmmo = 0;

        // 애니메이션 컨트롤러.
        [SerializeField]private PlayerAnimationController animationController;

        // 재장전할 때 재생할 소리 파일.
        [SerializeField] private AudioClip reloadWeaponClip;

        // 발사 간격 (단위: 초).
        [SerializeField] private float fireRate = 0.1f;
        // 다음에 발사가 가능한 시간을 저장할 변수.
        private float nextFireTime = 0f;

        // 재장전 이벤트.
        public UnityEvent OnReloadEvent;

        // 탄약의 수가 변경될 때 발생하는 이벤트.
        [SerializeField] private UnityEvent<int, int> OnAmmoChanged;

        // 발사가 가능한지 확인하는 프로퍼티.
        private bool CanFire { get { return currentAmmo > 0 && Time.time > nextFireTime; } }

        protected override void Awake()
        {
            base.Awake();

            // 시작할 때 탄약 가득 채우기.
            currentAmmo = data.maxAmmo;

            // 이벤트 발행.
            OnAmmoChanged?.Invoke(currentAmmo, data.maxAmmo);
        }

        public override void Fire()
        {
            base.Fire();

            // 발사가 가능하지 않으면 함수 종료.
            if (CanFire == false)
            {
                return;
            }

            // 다음에 발사가 가능한 시간 저장.
            nextFireTime = Time.time + fireRate;

            // 탄약 개수 감소 처리.
            --currentAmmo;

            // 이벤트 발행.
            OnAmmoChanged?.Invoke(currentAmmo, data.maxAmmo);

            // List / Dictionary.

            // 탄약 게임 오브젝트 생성.
            Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);

            // 발사 소리 재생.
            // 한번 재생.
            audioPlayer.PlayOneShot(fireSound);

            // 탄피 제거 효과 재생.
            cartridgeEjectEffect.Play();

            // 화염 효과 재생.
            muzzleFlashEffect.Play();

            // 카메라 흔들기.
            cameraShake.Play();

            // 재장전이 필요한지 확인.
            if (currentAmmo == 0)
            {
                // 재장전 처리.

                // 재장전 소리 재생.
                audioPlayer.PlayOneShot(reloadWeaponClip);

                // 재장전 애니메이션 재생.
                animationController.OnReload();
                // 재장전 이벤트 발행.
                //if (OnReloadEvent != null)
                //{
                //    OnReloadEvent.Invoke();
                //}

                OnReloadEvent?.Invoke();

                // 재장전 애니메이션 시간 만큼 대기 후 Reload 함수 실행.
                Invoke("Reload", animationController.WaitTimeToRelaod());
            }
        }

        // 재장전 함수.

        private void Reload()
        {
            // 탄약 채우기.
            currentAmmo = data.maxAmmo;

            // 이벤트 발행.
            OnAmmoChanged?.Invoke(currentAmmo, data.maxAmmo);
        }
    }
}