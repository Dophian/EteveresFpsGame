using UnityEngine;

namespace FPSGame
{
    
    // 사용자의 입력을 관리하는 입력 관리자 스크립트.
    public class PlayerInputManager : MonoBehaviour
    {
        // 입력 값을 저장하는 변수.
        public static float Horizontal { get; private set; } = 0f;
        public static float Vertical { get; private set; } = 0f;

        private void Update()
        {
            // 방향키 입력 저장.
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
        }
    }
}
