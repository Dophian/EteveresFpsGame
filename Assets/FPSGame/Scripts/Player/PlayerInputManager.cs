using UnityEngine;

namespace FPSGame
{
    
    // 사용자의 입력을 관리하는 입력 관리자 스크립트.
    public class PlayerInputManager : MonoBehaviour
    {
        // 입력 값을 저장하는 변수.
        public static float Horizontal { get; private set; } = 0f;
        public static float Vertical { get; private set; } = 0f;
        
        // 캐릭터 회전에 사용.
        public static float Turn { get; private set; } = 0f; // 좌우 드래그
        public static float Look { get; private set; } = 0f; // 상하 드래그

        // 마우스 클릭 이벤트 값.
        public static bool IsFire { get; private set; } = false;

        // 게임 오버.
        private bool isGameOver = false;

        private void Update()
        {
            // 게임오버인 경우, 업데이트 안함
            if (isGameOver)
            {
                return;
            }

            // 방향키 입력 저장.
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            // 마우스 드래그 값 저장.
            Turn = Input.GetAxis("Mouse X");
            Look = Input.GetAxis("Mouse Y");

            // 마우스 클릭 이벤트 값 저장.
            // 0: 왼쪽 버튼, 1:오른쪽 버튼, 2: 휠 버튼.
            IsFire = Input.GetMouseButton(0);
        }

        // 플레이어가 죽으면 실행될 리스너 메소드.
        public void OnPlayerDead()
        {
            isGameOver = true;

            // 입력 값 초기화.
            Horizontal = 0f;
            Vertical = 0f;
            Turn = 0f;
            Look = 0f;
            IsFire = false;
        }

    }
}
