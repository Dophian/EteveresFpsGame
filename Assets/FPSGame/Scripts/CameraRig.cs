using UnityEngine;

namespace FPSGame
{
    public class CameraRig : MonoBehaviour
    {
        [Header("플레이어 따라다니는 기능")]
        // 카메라가 따라다닐 대상.
        [SerializeField] private Transform target;

        // 이동할 때 적용할 지연(딜레이, Delay) 값.
        [SerializeField, Range(0.1f, 30f)] private float damping = 5f;
        
        // 회전할 때 적용할 지연(딜레이, Delay) 값.
        [SerializeField, Range(0.1f, 30f)] private float rotationdamping = 5f;

        private Transform refTransform;

        [Header("카메라 상하 드래그")]
        // 상하 회전에 사용하는 변수.
        [SerializeField] private Transform cameraTransform;     // 메인 카메라 트랜스폼.
        [SerializeField] private float minAngle = -30f;         // 상하 회전 최소 각도 값.
        [SerializeField] private float maxAngle = 40f;          // 상하 회전 최대 각도 값.
        [SerializeField] private float xRotation = 0f;          // 카메라의 x축 누적 회전을 계산하기 위한 변수.

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

            // 상하 회전 처리 함수 호출.
            Look();
        }

        // 상하 회전을 처리하는 함수
        private void Look()
        {
            // 하고 싶은 일: 카메라 x 회전을 위로 아래로 적용하기.
            // 카메라 트랜스폼 | 마우스 드래그 값(Y).

            // 마우스 위/아래 드래그 값을 -1에서 1 사이의 값으로 고정.
            float mouseY = Mathf.Clamp(PlayerInputManager.Look, -1f, 1f);

            // 마우스 드래그 값으로 x축 회전 누적.
            xRotation -= mouseY;

            // 회전 값 고정.
            xRotation = Mathf.Clamp(xRotation, minAngle, maxAngle);

            // 카메라의 오일러 회전 값 가져오기.
            Vector3 targetRotation = cameraTransform.localRotation.eulerAngles;
            targetRotation.x = xRotation;

            // 오일러 회전을 쿼터니언으로 변환한 후레 카메라 로컬 회전에 설정.
            cameraTransform.localRotation = Quaternion.Euler(targetRotation);
        }

        // 카메라의 x축 회전 값을 반환하는 함수.
        public float GetXRotation()
        {
            return xRotation;
        }
    }
}