using UnityEngine;

namespace FPSGame
{
    // �÷��̾��� ����(������Ʈ)�� �����ϴ� ������ ��ũ��Ʈ.
    public class PlayerStateManager : MonoBehaviour
    {
        // �÷��̾��� ���¸� ��Ÿ���� ������ ����.
        public enum State
        {
            Idle,
            Move,
            Dead,
            None
        }

        // ���� ����.
        [SerializeField] private State currentState = State.None;

        // ������Ʈ ������Ʈ �迭 ����.
        [SerializeField] private PlayerState[] states;

        // �ִϸ��̼� ��Ʈ�ѷ� ����.
        [SerializeField] private PlayerAnimationController animationController;

        // �÷��̾� ������.
        [SerializeField] private PlayerData data;

        // �÷��̾ �׾������� �˷��ִ� ������Ƽ.
        public bool IsPlayerDead { get { return currentState == State.Dead; } }

        private void OnEnable()
        {
            // ó�� ������ ������Ʈ ����.
            SetState(State.Idle);

            // �� ������Ʈ�� ������ ����.
            foreach (PlayerState state in states)
            {
                state.SetData(data);
            }
        }

        // ���� ���� �Լ�.
        public void SetState(State newState)
        {
            // ���� ó��.
            if (currentState == newState || currentState == State.Dead)
            {
                return;
            }

            if (currentState != State.None)
            {
                // ���� ���� ��ũ��Ʈ ����.
                states[(int)currentState].enabled = false;
            }

            // ���ο� ���� ��ũ��Ʈ �ѱ�.
            states[(int)newState].enabled = true;

            // ���� ���� ������Ʈ.
            currentState = newState;

            // �ִϸ��̼� ����.
            animationController.SetStateParameter((int)currentState);
        }
        private void Update()
        {
            // �Է��� ���� �� Ȯ��.
            if (PlayerInputManager.Horizontal == 0f
                && PlayerInputManager.Vertical == 0f)
            {
                // �Է��� ������ �⺻ ���·� ��ȯ.
                SetState(State.Idle);
            }
            else
            {
                // �̵� ���·� ��ȯ.
                SetState(State.Move);

                // �ִϸ��̼� ����.
                animationController.SetHorizontalParameter(PlayerInputManager.Horizontal);

                animationController.SetVerticalParameter(PlayerInputManager.Vertical);
            }
        }

        // �÷��̾ ������ ����Ǵ� �޼ҵ�(�޼���).
        public void OnPlayerDead()
        {
            SetState(State.Dead);
        }

    }
}