using System.Collections.Generic;
using Feature.NPC.Scripts.PatrolNodes;
using UnityEngine;
using UnityEngine.AI;

namespace Feature.NPC.Scripts.States
{
    public class PatrollingState : NpcBaseState
    {
        private int _currentNode = 0;
        private NavMeshAgent _agent;


        public override void OnEnterState(NpcStateController stateController)
        {
            base.OnEnterState(stateController);
            _agent = stateController.NavMeshAgent;
            _agent.isStopped = false;
                
            if(stateController.PatrolNodeController != null)
                _agent.SetDestination(stateController.PatrolNodeController.PatrolNodes[_currentNode].position);
        }

        public override void OnUpdate(NpcStateController stateController)
        {
            base.OnUpdate(stateController);
            
            if (stateController.PatrolNodeController == null)
                return;
            
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _currentNode++;
                if (_currentNode >= stateController.PatrolNodeController.PatrolNodes.Count)
                {
                    _currentNode = 0;
                }
                _agent.SetDestination(stateController.PatrolNodeController.PatrolNodes[_currentNode].position);
            }
            
        }
    }
}