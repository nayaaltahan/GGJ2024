using System;
using System.Collections.Generic;
using Feature.NPC.Scripts.PatrolNodes;
using Feature.NPC.Scripts.States;
using UnityEngine;
using UnityEngine.AI;

namespace Feature.NPC.Scripts
{
    
    public enum NpcState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }
    
    public class NpcStateController : MonoBehaviour
    {
        [SerializeField] private PatrolNodeController _patrolNodeController;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        
        public PatrolNodeController PatrolNodeController => _patrolNodeController;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public Animator NpcAnimator => _animator;
        
        private Dictionary<NpcState, NpcBaseState> _states;
        private NpcBaseState _currentState;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _states = new Dictionary<NpcState, NpcBaseState>();
            _states.Add(NpcState.Patrol, new PatrollingState());
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            // If current state is not dead, set this velocity:
            NpcAnimator.SetFloat(Speed, NavMeshAgent.velocity.magnitude);
            
            _currentState.OnUpdate(this);
        }
    }
}