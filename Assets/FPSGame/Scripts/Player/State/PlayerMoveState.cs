using UnityEngine;

namespace FPSGame
{
    // 이동 상태 스크립트.
    public class PlayerMoveState : PlayerState
    {
        // 이동 속도.
        [SerializeField] private float moveSpeed = 5f;

        protected override void Update()
        {
            base.Update();

            //이동 하고,
            Vector3 direction = new Vector3(PlayerInputManager.Horizontal, 0f, PlayerInputManager.Vertical);

            refTransform.position += direction.normalized * moveSpeed * Time.deltaTime;

        }
    }
}
