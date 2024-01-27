using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Feature.NPC.Scripts.States
{
    public class DeadState : NpcBaseState
    {
        public DeadState(NpcState type) : base(type)
        {
        }

        public override void OnEnterState(NpcStateController stateController)
        {
            base.OnEnterState(stateController);
            stateController.NpcAnimator.enabled = false;
            stateController.DisableNavMeshAgent();
            stateController.RagdollController.ActivateRagdoll();
        }

        public override void OnExitState(NpcStateController stateController)
        {
            base.OnExitState(stateController);
            stateController.NpcAnimator.enabled = true;
        }
    }
}