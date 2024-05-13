using UnityEngine;

namespace FPSGame
{
    public class PlayerControl : MonoBehaviour
    {
        // 이동 속도.
        [SerializeField] private float moveSpeed = 5f;
        
        // 트랜스폼 컴포넌트 참조 변수.
        private Transform refTransform;

        private void Awake()
        {
            // 트랜스폼 참조 저장.
            refTransform = transform;
        }

        private void Update()
        {
            // 방향키 입력 받기.
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // 이동.
            refTransform.position +=
                new Vector3(horizontal, 0f, vertical).normalized * moveSpeed * Time.deltaTime;
        }
    }
}