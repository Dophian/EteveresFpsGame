using UnityEngine;

namespace FPSGame
{
    // 게임 관리자 스크립트 - 게임의 점수 관리.
    public class GameManager : Singleton<GameManager>
    {
        // 필드.
        [SerializeField] private int score = 0;
        [SerializeField] private TMPro.TextMeshProUGUI scoreText;
        [SerializeField] private TMPro.TextMeshProUGUI fpsText;

        private void Update()
        {
            if (fpsText != null)
            {
                fpsText.text = $"FPS: {(int)(1.0f / Time.deltaTime)}";
            }

            // ESC키를 누르면 게임 종료.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                // 게임 종료 명령어.
                Application.Quit();
#endif
            }
        }

        // 메세지.
        // 점수 획득 메세지(공개 메소드).
        public void AddScore()
        {
            score = score + 1;

            // 점수 텍스트 업데이트.
            scoreText.text = $"{score.ToString("0000")}KILL";
        }
    }
}
