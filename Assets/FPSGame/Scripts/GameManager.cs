using UnityEngine;

namespace FPSGame
{
    // 게임 관리자 스크립트 - 게임의 점수 관리.
    public class GameManager : Singleton<GameManager>
    {
        // 필드.
        [SerializeField] private int score = 0;
        [SerializeField] private TMPro.TextMeshProUGUI scoreText;

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
