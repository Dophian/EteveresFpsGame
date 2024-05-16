using UnityEngine;

namespace FPSGame
{
    // �÷��̾� ����(������Ʈ)�� ��� Ŭ����.
    // ������ ������-������Ʈ-���� �޼ҵ� �� ���� ��� ����.
    public class PlayerState : MonoBehaviour
    {
        // Ʈ������ ���� ����.
        protected Transform refTransform;

        // ĳ���� ��Ʈ�ѷ� ������Ʈ ���� ����.
        [SerializeField] private CharacterController characterController;

        // ���� ���� �Լ�.
        protected virtual void OnEnable()
        {
            // Ʈ������ ������Ʈ �ʱ�ȭ.
            if (refTransform == null)
            {
                refTransform = transform;
            }

            // ������Ʈ �ʱ�ȭ.
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
        }

        // ���� ������Ʈ �Լ�.
        protected virtual void Update()
        {

        }

        // ���� ���� �Լ�.
        protected virtual void OnDisable()
        {

        }
    }
}