using System;
using System.Collections.Generic;
using DefaultNamespace;
using Feature.NPC.Scripts.PatrolNodes;
using Feature.NPC.Scripts.Scriptable_Objects;
using Feature.NPC.Scripts.States;
using Feature.Ragdoll;
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
        [SerializeField] private Transform _attackFromTransform;
        [SerializeField] private NpcState _defaultState = NpcState.Patrol;


        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private RagdollController _ragdollController;
        private Transform _playerTransform;
        public Vector3 TargetPosition;
        public Vector3 RandomPoint;

        public PatrolNodeController PatrolNodeController => _patrolNodeController;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public Animator NpcAnimator => _animator;
        public NpcSettings Settings => _settings;
        public RagdollController RagdollController => _ragdollController;
        public NpcBaseState PreviousState => _previousState;
        public Transform PlayerTransform => _playerTransform;
        public Transform AttackFromTransform => _attackFromTransform;
        

        [ShowInInspector, ReadOnly]
        private NpcBaseState _currentState;
        [ShowInInspector, ReadOnly]
        private NpcBaseState _previousState;
        
        private Dictionary<NpcState, NpcBaseState> _states;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            InitializeStates();
            GetComponents();


            SetState(_defaultState);
        }

        private void Start()
        {
            _playerTransform = PlayerManager.Instance.PlayerTransform;
        }

        private void GetComponents()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _ragdollController = GetComponent<RagdollController>();
        }

        private void InitializeStates()
        {
            _states = new Dictionary<NpcState, NpcBaseState>();
            _states.Add(NpcState.Idle, new IdleState(NpcState.Idle));
            _states.Add(NpcState.Patrol, new PatrollingState(NpcState.Patrol));
            _states.Add(NpcState.Dead, new DeadState(NpcState.Dead));
            _states.Add(NpcState.Chase, new ChasingState(NpcState.Chase));
            _states.Add(NpcState.Attack, new AttackingState(NpcState.Attack));
        }

        private void Update()
        {
            // If current state is not dead, set this velocity:
            if (_currentState.Type != NpcState.Dead)
                NpcAnimator.SetFloat(Speed, NavMeshAgent.velocity.magnitude);

            _currentState.OnUpdate(this);
        }

        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        public void SetState(NpcState state)
        {
            if (_currentState != null)
                _previousState = _currentState;
            
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
        
        
        public void SetAgentAlertSettings()
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = Settings.AlertSpeed;
            _navMeshAgent.angularSpeed = Settings.AlertRotationSpeed;
            _navMeshAgent.stoppingDistance = Settings.AlertStoppingDistance;
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

        private void OnDrawGizmosSelected()
        {
            _currentState?.OnDrawGizmosSelected(this);
        }
    }
}