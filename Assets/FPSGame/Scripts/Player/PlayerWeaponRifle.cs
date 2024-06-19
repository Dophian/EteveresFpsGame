using UnityEngine;
using UnityEngine.Events;

namespace FPSGame
{
    public class PlayerWeaponRifle : PlayerWeapon
    {
        // ź�� �߻縦 ���� ����.
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform muzzleTransform;

        // �߻��� �� �Ҹ� ����� ���� ����.
        [SerializeField] private AudioSource audioPlayer;
        [SerializeField] private AudioClip fireSound;

        // ź�� ���� ȿ�� ��ƼŬ.
        //[SerializeField] private Transform cartridgeEjectTransform;
        [SerializeField] private ParticleSystem cartridgeEjectEffect;
        [SerializeField] private ParticleSystem muzzleFlashEffect;

        // ī�޶� ����.
        [SerializeField] private CameraShake cameraShake;

        // �÷��̾� ������.
        [SerializeField] private PlayerData data;

        // ���� ���� ź�� ��.
        [SerializeField] private int currentAmmo = 0;

        // �ִϸ��̼� ��Ʈ�ѷ�.
        [SerializeField]private PlayerAnimationController animationController;

        // �������� �� ����� �Ҹ� ����.
        [SerializeField] private AudioClip reloadWeaponClip;

        // �߻� ���� (����: ��).
        [SerializeField] private float fireRate = 0.1f;
        // ������ �߻簡 ������ �ð��� ������ ����.
        private float nextFireTime = 0f;

        // ������ �̺�Ʈ.
        public UnityEvent OnReloadEvent;

        // ź���� ���� ����� �� �߻��ϴ� �̺�Ʈ.
        [SerializeField] private UnityEvent<int, int> OnAmmoChanged;

        // �߻簡 �������� Ȯ���ϴ� ������Ƽ.
        private bool CanFire { get { return currentAmmo > 0 && Time.time > nextFireTime; } }

        protected override void Awake()
        {
            base.Awake();

            // ������ �� ź�� ���� ä���.
            currentAmmo = data.maxAmmo;

            // �̺�Ʈ ����.
            OnAmmoChanged?.Invoke(currentAmmo, data.maxAmmo);
        }

        public override void Fire()
        {
            base.Fire();

            // �߻簡 �������� ������ �Լ� ����.
            if (CanFire == false)
            {
                return;
            }

            // ������ �߻簡 ������ �ð� ����.
            nextFireTime = Time.time + fireRate;

            // ź�� ���� ���� ó��.
            --currentAmmo;

            // �̺�Ʈ ����.
            OnAmmoChanged?.Invoke(currentAmmo, data.maxAmmo);

            // List / Dictionary.

            // ź�� ���� ������Ʈ ����.
            Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);

            // �߻� �Ҹ� ���.
            // �ѹ� ���.
            audioPlayer.PlayOneShot(fireSound);

            // ź�� ���� ȿ�� ���.
            cartridgeEjectEffect.Play();

            // ȭ�� ȿ�� ���.
            muzzleFlashEffect.Play();

            // ī�޶� ����.
            cameraShake.Play();

            // �������� �ʿ����� Ȯ��.
            if (currentAmmo == 0)
            {
                // ������ ó��.

                // ������ �Ҹ� ���.
                audioPlayer.PlayOneShot(reloadWeaponClip);

                // ������ �ִϸ��̼� ���.
                animationController.OnReload();
                // ������ �̺�Ʈ ����.
                //if (OnReloadEvent != null)
                //{
                //    OnReloadEvent.Invoke();
                //}

                OnReloadEvent?.Invoke();

                // ������ �ִϸ��̼� �ð� ��ŭ ��� �� Reload �Լ� ����.
                Invoke("Reload", animationController.WaitTimeToRelaod());
            }
        }

        // ������ �Լ�.

        private void Reload()
        {
            // ź�� ä���.
            currentAmmo = data.maxAmmo;

            // �̺�Ʈ ����.
            OnAmmoChanged?.Invoke(currentAmmo, data.maxAmmo);
        }
    }
}