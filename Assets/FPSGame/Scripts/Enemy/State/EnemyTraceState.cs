namespace FPSGame
{
    // �� ĳ���Ͱ� �÷��̾ �i�ư� �� ����ϴ� ���� ��ũ��Ʈ.
    public class EnemyTraceState : EnemyState
    {
        // �÷��̾� ���� -> ���� �����ڿ��� �޾ƿ�.
        // �÷��̾��� ��ġ.
        // �÷��̾ ����ִ� �� ����.

        protected override void OnEnable()
        {
            base.OnEnable();

            // �÷��̾��� ��ġ ������Ʈ.
            UpdatePlayerPosition();

            // Todo: �÷��̾ ��� �ִ��� Ȯ�� �� �׾����� ������ �ٷ� ��ȯ.
            if (manager.IsPlayerDead)
            {
                manager.SetState(EnemyStateManager.State.Idle);
                
            }
        }

        protected override void Update()
        {
            base.Update();

            // �÷��̾ ��� �ִ��� Ȯ�� �� �׾����� ������ �ٷ� ��ȯ.
            if (manager.IsPlayerDead)
            {
                manager.SetState(EnemyStateManager.State.Idle);
                return;
            }

            // �÷��̾��� ��ġ ������Ʈ �� ���󰡱�.
            UpdatePlayerPosition();

            // ���� ���� ������ ����������, ���� ���·� ��ȯ.
            if (manager.Agent.remainingDistance <= data.AttackDistance)
            {
                manager.SetState(EnemyStateManager.State.Attack);
            }
        }

        // �÷��̾��� ��ġ�� ������ NavMeshAgent�� �������� �����ϴ� �޼ҵ�.
        private void UpdatePlayerPosition()
        {
            manager.SetAgentDestination(
                manager.PlayerTransform.position,
                data.TraceSpeed
            );
        }
    }
}