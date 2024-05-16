using UnityEngine;

namespace FPSGame
{
    public class CameraRig : MonoBehaviour
    {
        // 카메라가 따라다닐 대상.
        [SerializeField] private Transform target;

        // 이동할 때 적용할 지연(딜레이, Delay) 값.
        [SerializeField] private float damping = 100f;
        
        // 회전할 때 적용할 지연(딜레이, Delay) 값.
        [SerializeField] private float rotationdamping = 100f;

        private Transform refTransform;

        private void Awake()
        {
            refTransform = transform;
            
            // 커서 락.
            Cursor.lockState = CursorLockMode.Locked;
        }

        // 매 프레임 실행됨. Update 보다 실행 시점이 느림.
        private void LateUpdate()
        {
            // Lerp.
            refTransform.position = Vector3.Lerp(
                refTransform.position,
                target.position,
                damping * Time.deltaTime
            );

            // 회전 (Lerp).
            //Quaternion.Lerp
            //    Quaternion.Slerp 
            refTransform.rotation = Quaternion.Lerp(
                refTransform.rotation,
                target.rotation,
                rotationdamping * Time.deltaTime
                );
        }
    }
}