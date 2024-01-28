using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Feature.NPC.Scripts.PatrolNodes
{
    [ExecuteInEditMode]
    public class DrawPatrolNodeGizmo : MonoBehaviour
    {
        PatrolNodeController _patrolNodeController;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_patrolNodeController == null)
                _patrolNodeController = GetComponentInParent<PatrolNodeController>();
            Gizmos.color = Color.red;
            for (int i = 0; i < _patrolNodeController.PatrolNodes.Count; i++)
            {
                if (i == _patrolNodeController.PatrolNodes.Count - 1)
                {
                    // Draw line from last node to first node
                    Gizmos.DrawLine(_patrolNodeController.PatrolNodes[i].position, _patrolNodeController.PatrolNodes[0].position);
                }
                else
                {
                    // Draw line from current node to next node
                    Gizmos.DrawLine(_patrolNodeController.PatrolNodes[i].position, _patrolNodeController.PatrolNodes[i + 1].position);
                }
            }
        }
#endif
    }
}