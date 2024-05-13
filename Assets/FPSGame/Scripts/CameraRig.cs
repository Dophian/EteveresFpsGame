using UnityEngine;

namespace FPSGame
{
    public class CameraRig : MonoBehaviour
    {
        // ī�޶� ����ٴ� ���.
        [SerializeField] private Transform target;

        // �̵��� �� ������ ����(������, Delay) ��.
        [SerializeField] private float damping = 5f;

        private Transform refTransform;

        private void Awake()
        {
            refTransform = transform;
        }

        // �� ������ �����. Update ���� ���� ������ ����.
        private void LateUpdate()
        {
            // Lerp.
            refTransform.position = Vector3.Lerp(
                refTransform.position,
                target.position,
                damping * Time.deltaTime
            );
        }
    }
}