using UnityEngine;

namespace Feature.NPC.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "GGJ2024", menuName = "ScriptableObjects/NPCSettings", order = 1)]
    public class NpcSettings : ScriptableObject
    {

        [SerializeField]
        private float _calmSpeed = 3.5f;
        
        [SerializeField]
        private float _alertSpeed = 5f;
        
        [SerializeField]
        private float _calmRotationSpeed = 120f;
        
        [SerializeField]
        private float _alertRotationSpeed = 170f;
        
        [SerializeField]
        private float _calmStoppingDistance = 1f;
        
        [SerializeField]
        private float _alertStoppingDistance = 2f; 
        
        [SerializeField]
        private float _startAttackRange = 1f; 
        
        [SerializeField]
        private float _attackCooldown = 1f;

        [SerializeField] private int _attackProjectileDelay = 500;


        [SerializeField] private LayerMask _attackLayer;
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private float _exitAttackStateRange = 3.5f;
        [SerializeField] private float _attackDamage = 100f;
        
        
        public float CalmSpeed => _calmSpeed;
        public float AlertSpeed => _alertSpeed;
        public float CalmRotationSpeed => _calmRotationSpeed;
        public float AlertRotationSpeed => _alertRotationSpeed;
        public float CalmStoppingDistance => _calmStoppingDistance;
        public float AlertStoppingDistance => _alertStoppingDistance;
        public float StartAttackRange => _startAttackRange;
        public float AttackCooldown => _attackCooldown;
        public LayerMask AttackLayer => _attackLayer;
        public float AttackRange => _attackRange;
        public int AttackProjectileDelay => _attackProjectileDelay;
        public float PatrolToAttackDistance => _attackRange - 0.2f;
        public float ExitAttackStateRange => _exitAttackStateRange;
        public float AttackDamage => _attackDamage;
    }
}