using System;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.NPC.Scripts.PatrolNodes
{
    public class PatrolNodeController : MonoBehaviour
    {
        public List<Transform> PatrolNodes;

        private void OnGUI()
        {
            // Draw lines between the patrol nodes
            for (int i = 0; i < PatrolNodes.Count; i++)
            {
                if (i == PatrolNodes.Count - 1)
                {
                    // Draw line from last node to first node
                    Debug.DrawLine(PatrolNodes[i].position, PatrolNodes[0].position, Color.red);
                }
                else
                {
                    // Draw line from current node to next node
                    Debug.DrawLine(PatrolNodes[i].position, PatrolNodes[i + 1].position, Color.red);
                }
            }
        }
    }
}