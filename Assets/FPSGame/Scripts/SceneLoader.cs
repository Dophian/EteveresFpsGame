using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPSGame
{
    // Start 씬에서 Game씬을 로드하는 스크립트.
    public class SceneLoader : MonoBehaviour
    {
        // 로드할 씬의 인덱스 값.
        [SerializeField] private int sceneIndexToLoad;

        // 대기할 최소 시간.
        [SerializeField] private float waitTimeToStart = 3f;

        // 씬 로드의 진행률을 보여줄 Image 컴포넌트.
        [SerializeField] private Image progressBar;

        // 씬을 비동기 방식으로 로드했을 때 중간 과정 정보를 읽을 수 있는 타입.
        private AsyncOperation asyncOperation;
        private float elapsedTime = 0f;

        private void OnEnable()
        {
            // 씬을 로드 (동기(Synchronized) 방식).
            //SceneManager.LoadScene(sceneIndexToLoad);

            StartCoroutine(LoadScene());
        }

        private void Update()
        {
            // 씬 로드가 완료되면, 진행률을 확인.
            if (asyncOperation != null && asyncOperation.progress >= 0.9f)
            {
                // 진행률을 뻥으로 만듦.
                elapsedTime += Time.deltaTime;
                //Debug.Log(elapsedTime / waitTimeToStart);

                // 진행률 보여주기.
                progressBar.fillAmount = elapsedTime / waitTimeToStart;

                // 원하는 시간만큼 지났으면 씬 로드.
                if (elapsedTime > waitTimeToStart)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
        }

        private IEnumerator LoadScene()
        {
            // 강제로 화면 유지.
            yield return new WaitForSeconds(1f);

            // 비동기(Asynchronized)로 씬을 로드.
            asyncOperation = SceneManager.LoadSceneAsync(sceneIndexToLoad);

            // 씬을 로드했을 때 곧바로 활성화할 지 여부를 지정하는 속성.
            asyncOperation.allowSceneActivation = false;

            // 비동기 로드 객체 대기.
            yield return asyncOperation;
        }
    }
}
