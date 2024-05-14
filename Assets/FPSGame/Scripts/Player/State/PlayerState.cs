using UnityEngine;

namespace FPSGame
{
    // �÷��̾� ����(������Ʈ)�� ��� Ŭ����.
    // ������ ������-������Ʈ-���� �޼ҵ� �� ���� ��� ����.
    public class PlayerState : MonoBehaviour
    {
        protected Transform refTransform;

        // ���� ���� �Լ�.
        protected virtual void OnEnable()
        {
            // Ʈ������ ������Ʈ �ʱ�ȭ.
            if (refTransform == null)
            {
                refTransform = transform;
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