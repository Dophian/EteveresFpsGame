using UnityEngine;

namespace FPSGame
{
    // 이동 상태 스크립트.
    public class PlayerMoveState : PlayerState
    {
        // 이동 속도.
        //[SerializeField] private float moveSpeed = 5f;

        protected override void Update()
        {
            base.Update();

            Vector3 direction =
                refTransform.right * PlayerInputManager.Horizontal
                + refTransform.forward * PlayerInputManager.Vertical;

            //이동 하고,
            //Vector3 direction = new Vector3(PlayerInputManager.Horizontal, 0f, PlayerInputManager.Vertical);

            //refTransform.position += direction.normalized * moveSpeed * Time.deltaTime;

            // 캐릭터 컨트롤러를 사용한 이동
            characterController.Move(direction.normalized * data.moveSpeed * Time.deltaTime);
        }
    }
}
