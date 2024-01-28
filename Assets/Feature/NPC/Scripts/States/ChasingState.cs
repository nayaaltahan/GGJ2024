
using UnityEngine;

namespace Feature.NPC.Scripts.States
{
    public class ChasingState : NpcBaseState
    {
        public ChasingState(NpcState type) : base(type)
        {
        }

        public override void OnEnterState(NpcStateController stateController)
        {
            base.OnEnterState(stateController);
            stateController.RandomPoint = Random.insideUnitSphere;
            stateController.RandomPoint.y = stateController.PlayerTransform.position.y;
            stateController.SetAgentAlertSettings();
            stateController.EnableNavMeshAgent();
            stateController.TargetPosition = stateController.PlayerTransform.position + stateController.RandomPoint * stateController.Settings.PatrolToAttackDistance;
            stateController.NavMeshAgent.SetDestination(stateController.TargetPosition);
        }

        public override void OnUpdate(NpcStateController stateController)
        {
            base.OnUpdate(stateController);
            // var destination = stateController.PlayerTransform.position - (stateController.PlayerTransform.position - stateController.transform.position).normalized * stateController.Settings.StartAttackRange;
            
            // Set destination to random point around player
            stateController.TargetPosition = stateController.PlayerTransform.position + stateController.RandomPoint * stateController.Settings.PatrolToAttackDistance;
            
            stateController.NavMeshAgent.SetDestination(stateController.TargetPosition);
            if (Vector3.Distance(stateController.TargetPosition, stateController.transform.position) <= stateController.Settings.PatrolToAttackDistance)
            {
                stateController.SetState(NpcState.Attack);
            }
        }

        public override void OnDrawGizmosSelected(NpcStateController stateController)
        {
            base.OnDrawGizmosSelected(stateController);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(stateController.PlayerTransform.position + stateController.RandomPoint * stateController.Settings.StartAttackRange, .2f);
            
        }
    }
}