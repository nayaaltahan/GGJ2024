using System;
using Feature.NPC.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class BananaPeel : MonoBehaviour
    {
        [SerializeField] private float _forceToApply = 100f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("NPCHitCollider"))
            {
                var stateController = other.gameObject.GetComponentInParent<NpcStateController>();
                stateController.SetDeadState();
                stateController.RagdollController.AddForceToFoot(stateController.transform.forward + Vector3.up * _forceToApply);
                Destroy(gameObject);
            }
            Debug.Log("Collided with " + other.gameObject.name, gameObject);
        }
    }
}