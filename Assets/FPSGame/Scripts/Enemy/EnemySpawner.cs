using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    // 적 캐릭터를 생성하는 시스템.
    public class EnemySpawner : MonoBehaviour
    {
        // 필드.
        // 생성 지점(위치).
        [SerializeField] private Transform spawnGroup;
        [SerializeField] private List<Transform> spawnPoints;

        // 적 캐릭터 프리팹.
        [SerializeField] private GameObject enemyPrefab;

        // 생성 간격 (시간, 단위: 초).
        [SerializeField] private float spawnTime = 3f;

        // 월드 한번에 배치가 가능한 적 캐릭터의 최대 수 (제한 값).
        [SerializeField] private int maxEnemyCount = 10;

        // 기타.
        private bool isPlayerDead = false;

        // 메소드.
        // 초기화.
        private void OnEnable()
        {
            if (spawnGroup != null)
            {
                // spawnGroup 하위에 있는 Transform 컴포넌트를 리스트에 저장.
                spawnGroup.GetComponentsInChildren<Transform>(spawnPoints);

                // spawnGroup은 생성 위치 데이터로 필요가 없기에 제거.
                spawnPoints.RemoveAt(0);
            }

            // 이벤트 등록.
            var playerDamageController = FindFirstObjectByType<PlayerDamageController>();
            if (playerDamageController != null)
            {
                playerDamageController.SubscribeOnPlayerDead(OnPlayerDead);
            }

            // 생성 시작.
            StartCoroutine(SpawnEnemy());
        }

        // 이벤트 리스너 메소드.
        private void OnPlayerDead()
        {
            isPlayerDead = true;
        }

        // 생성 메소드.
        private IEnumerator SpawnEnemy()
        {
            // 플레이어가 살아 있으면 계속 반복.
            while (isPlayerDead == false)
            {
                // 현재 맵에 배치된 적의 수 확인.
                int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

                // 배치된 적의 수가 허용 가능 수보다 작으면 생성.
                if (enemyCount < maxEnemyCount)
                {
                    // 일정 시간 대기.
                    yield return new WaitForSeconds(
                        Random.Range(spawnTime - 0.5f, spawnTime + 0.5f)
                    );

                    // 생성.
                    int index = Random.Range(0, spawnPoints.Count);
                    Instantiate(
                        enemyPrefab,
                        spawnPoints[index].position,
                        Quaternion.identity
                    );
                }
                else
                {
                    // 일정 시간 대기.
                    yield return new WaitForSeconds(
                        Random.Range(spawnTime - 0.5f, spawnTime + 0.5f)
                    );
                }
            }
        }
    }
}
