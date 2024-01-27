using System;
using System.Collections.Generic;
using Feature.NPC.Scripts.PatrolNodes;
using Feature.NPC.Scripts.Scriptable_Objects;
using Feature.NPC.Scripts.States;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Feature.NPC.Scripts
{
    public enum NpcState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    [SelectionBase]
    public class NpcStateController : MonoBehaviour
    {
        [SerializeField] private PatrolNodeController _patrolNodeController;
        [SerializeField] private NpcSettings _settings;
        [SerializeField] private NpcState _defaultState = NpcState.Patrol;


        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private NpcSetup _setup;

        public PatrolNodeController PatrolNodeController => _patrolNodeController;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public Animator NpcAnimator => _animator;
        public NpcSettings Settings => _settings;
        public NpcSetup Setup => _setup;

        private Dictionary<NpcState, NpcBaseState> _states;
        [ReadOnly] private NpcBaseState _currentState;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            InitializeStates();

            GetComponents();

            SetState(_defaultState);
        }

        private void GetComponents()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _setup = GetComponent<NpcSetup>();
        }

        private void InitializeStates()
        {
            _states = new Dictionary<NpcState, NpcBaseState>();
            _states.Add(NpcState.Idle, new IdleState(NpcState.Idle));
            _states.Add(NpcState.Patrol, new PatrollingState(NpcState.Patrol));
            _states.Add(NpcState.Dead, new DeadState(NpcState.Dead));
        }

        private void Update()
        {
            // If current state is not dead, set this velocity:
            if (_currentState.Type != NpcState.Dead)
                NpcAnimator.SetFloat(Speed, NavMeshAgent.velocity.magnitude);

            _currentState.OnUpdate(this);
        }

        [Button]
        public void SetState(NpcState state)
        {
            _currentState?.OnExitState(this);
            _currentState = _states[state];
            _currentState.OnEnterState(this);
        }

        public void SetAgentCalmSettings()
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = Settings.CalmSpeed;
            _navMeshAgent.angularSpeed = Settings.CalmRotationSpeed;
            _navMeshAgent.stoppingDistance = Settings.CalmStoppingDistance;
        }

        public void DisableNavMeshAgent()
        {
            _navMeshAgent.isStopped = true;
        }

        public void ReturnToDefaultState()
        {
            SetState(_defaultState);
        }

        public void EnableNavMeshAgent()
        {
            _navMeshAgent.isStopped = false;
        }
    }
}