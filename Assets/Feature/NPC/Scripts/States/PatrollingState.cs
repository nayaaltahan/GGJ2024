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
            stateController.SetAgentCalmSettings();
            
            _agent = stateController.NavMeshAgent;
            _currentNode = FindClosestNode(stateController);

            _agent.SetDestination(stateController.PatrolNodeController.PatrolNodes[_currentNode].position);
            stateController.EnableNavMeshAgent();
        }

        public override void OnUpdate(NpcStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.PatrolNodeController == null)
                return;

            if (_agent.remainingDistance <= _agent.stoppingDistance + 0.5f)
            {
                _currentNode++;
                if (_currentNode >= stateController.PatrolNodeController.PatrolNodes.Count)
                {
                    _currentNode = 0;
                }

                _agent.SetDestination(stateController.PatrolNodeController.PatrolNodes[_currentNode].position);
            }
        }
        
        private int FindClosestNode(NpcStateController stateController)
        {
            var closestNode = 0;
            var closestDistance = Mathf.Infinity;
            if (stateController.PatrolNodeController != null)
            {
                for (var i = 0; i < stateController.PatrolNodeController.PatrolNodes.Count; i++)
                {
                    var distance = Vector3.Distance(stateController.PatrolNodeController.PatrolNodes[i].position,
                        stateController.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestNode = i;
                    }
                }
            }

            return closestNode;
        }

        public PatrollingState(NpcState type) : base(type)
        {
            
        }
    }
}