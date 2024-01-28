using System;

namespace Feature.NPC.Scripts
{
    [Serializable]
    public class NpcBaseState
    {
        public NpcState Type;

        public NpcBaseState(NpcState type)
        {
            Type = type;
        }

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

        public virtual void OnDrawGizmosSelected(NpcStateController stateController)
        {
            
        }
    }
}