using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Feature.NPC.Scripts.PatrolNodes
{
    public class PatrolNodeController : MonoBehaviour
    {
        public List<Transform> PatrolNodes;
        public GameObject PatrolNode;


        [Button("Add patrol node")]
        private void AddPatrolNode()
        {
            var newPatrolNode = Instantiate(PatrolNode, transform.position, Quaternion.identity, transform);
            PatrolNodes.Add(newPatrolNode.transform);
        }
        
        private void OnDrawGizmosSelected()
        {
            // Draw lines between the patrol nodes
            Gizmos.color = Color.red;
            for (int i = 0; i < PatrolNodes.Count; i++)
            {
                if (i == PatrolNodes.Count - 1)
                {
                    // Draw line from last node to first node
                    Gizmos.DrawLine(PatrolNodes[i].position, PatrolNodes[0].position);
                }
                else
                {
                    // Draw line from current node to next node
                    Gizmos.DrawLine(PatrolNodes[i].position, PatrolNodes[i + 1].position);
                }
            }
        }

    }
}