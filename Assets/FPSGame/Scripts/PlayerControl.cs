using UnityEngine;

namespace FPSGame
{
    public class PlayerControl : MonoBehaviour
    {
        // �̵� �ӵ�.
        [SerializeField] private float moveSpeed = 5f;
        
        // Ʈ������ ������Ʈ ���� ����.
        private Transform refTransform;

        private void Awake()
        {
            // Ʈ������ ���� ����.
            refTransform = transform;
        }

        private void Update()
        {
            // ����Ű �Է� �ޱ�.
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // �̵�.
            refTransform.position +=
                new Vector3(horizontal, 0f, vertical).normalized * moveSpeed * Time.deltaTime;
        }
    }
}