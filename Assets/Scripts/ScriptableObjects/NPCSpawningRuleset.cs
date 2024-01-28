using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NPCSpawningRuleset", menuName = "ScriptableObjects/NPCSpawningRuleset", order = 0)]
    public class NPCSpawningRuleset : ScriptableObject
    {
        public GameObject NPCPrefab;
        public float spawningInterval;

    }
}