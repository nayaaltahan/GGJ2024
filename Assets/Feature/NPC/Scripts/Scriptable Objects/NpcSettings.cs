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
        
        public float CalmSpeed => _calmSpeed;
        public float AlertSpeed => _alertSpeed;
        public float CalmRotationSpeed => _calmRotationSpeed;
        public float AlertRotationSpeed => _alertRotationSpeed;
        public float CalmStoppingDistance => _calmStoppingDistance;
        public float AlertStoppingDistance => _alertStoppingDistance;
        
    }
}