namespace Feature.NPC.Scripts.States
{
    public class IdleState : NpcBaseState
    {
        public IdleState(NpcState type) : base(type)
        {
            
        }

        public override void OnEnterState(NpcStateController stateController)
        {
            base.OnEnterState(stateController);
            stateController.DisableNavMeshAgent();
        }

        public override void OnExitState(NpcStateController stateController)
        {
            base.OnExitState(stateController);
        }
    }
}