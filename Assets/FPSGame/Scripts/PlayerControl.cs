using UnityEngine;

namespace FPSGame
{
    public class PlayerControl : MonoBehaviour
    {
        // �̵� �ӵ�.
        [SerializeField] private float moveSpeed = 5f;

        // Animator ������Ʈ ����.
        [SerializeField] private Animator refAnimator;

        // Ʈ������ ������Ʈ ���� ����.
        private Transform refTransform;

        private void Awake()
        {
            // Ʈ������ ���� ����.
            refTransform = transform;

            // ���� ����.
            refAnimator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            // ����Ű �Է� �ޱ�.
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // �ִϸ��̼� ����.
            if (horizontal == 0f && vertical == 0f)
            {
                // �Է��� ����.
                refAnimator.SetInteger("State", 0);
            }
            else
            {
                // �Է��� ����.
                refAnimator.SetInteger("State", 1);
            }

            // �̵�.
            refTransform.position +=
                new Vector3(horizontal, 0f, vertical).normalized * moveSpeed * Time.deltaTime;
        }
    }
}