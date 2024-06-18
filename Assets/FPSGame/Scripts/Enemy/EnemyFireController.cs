using UnityEngine;


namespace FPSGame
{
    // 적 캐릭터가 발사할 때 사용하는 스크립트.
    public class EnemyFireController : MonoBehaviour
    {
        // 필드.
        // 탄약을 발사할 때 소리를 재생시키는 컴포넌트.
        [SerializeField] private AudioSource audioPlayer;

        // 탄약을 발사할 때 재생할 오디오 클립(오디오 파일).
        [SerializeField] private AudioClip fireClip;

        // 장전할 때 재생할 오디오 클립(오디오 파일).
        [SerializeField] private AudioClip reloadClip;

        // 탄약 프리팹.
        [SerializeField] private GameObject bulletPrefab;

        // 탄약 발사에 사용할 총구 위치.
        [SerializeField] private Transform firePosition;

        // 총구 화염 효과.
        [SerializeField] private ParticleSystem muzzleFlashEffect;

        // 플레이어 트랜스폼.
        // 탄약을 발사할 때 플레이어를 향하도록 하는 데 활용.
        private Transform playerTransform;

        private void OnEnable()
        {
            // 초기화.
            if (audioPlayer == null)
            {
                audioPlayer = GetComponent<AudioSource>();
            }

            if (playerTransform == null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        // 발사할 때 실행할 함수.
        public void Fire()
        {
            // 프리팹을 사용해서 탄약 생성.
            // 위치, 회전 값이 필요함.
            // 회전 값은 구하기 - 플레이어를 향하도록.
            Vector3 playerPosition = playerTransform.position;
            // Y(높이) 보정 -> 플레이어의 위치는 바닥면을 기준으로 하기 때문에 보정이 필요함.
            playerPosition.y = firePosition.position.y;
            Vector3 direction = playerPosition - firePosition.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            // 탄약 생성.
            GameObject bullet = Instantiate(bulletPrefab, firePosition.position, rotation);

            // 제거 예약.
            Destroy(bullet, 3f);

            // 효과 재생.
            audioPlayer.PlayOneShot(fireClip);

            // 이펙트 재생.
            muzzleFlashEffect.Play();
        }

        // 장전 함수.
        public void Reload()
        {
            audioPlayer.PlayOneShot(reloadClip);
        }

    }
}
