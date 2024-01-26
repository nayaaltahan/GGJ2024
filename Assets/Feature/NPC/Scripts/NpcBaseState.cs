namespace Feature.NPC.Scripts
{
    public class NpcBaseState
    {
        public virtual void OnEnterState(NpcStateController stateController)
        {
            
        }
        
        public virtual void OnExitState(NpcStateController stateController)
        {
            
        }

        public virtual void OnUpdate(NpcStateController stateController)
        {
            
        }
        
        public virtual void OnFixedUpdate(NpcStateController stateController)
        {
            
        }
        
        public virtual void SwitchState(NpcStateController stateController, NpcBaseState newState)
        {
            OnExitState(stateController);   
        }
    }
}