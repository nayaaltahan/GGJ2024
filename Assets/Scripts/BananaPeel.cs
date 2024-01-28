using System;
using Feature.NPC.Scripts;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class BananaPeel : MonoBehaviour
    {
        [SerializeField] private float _forceToApply = 100f;
        [SerializeField] private EventReference _slatSound;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("NPCHitCollider"))
            {
                var stateController = other.gameObject.GetComponentInParent<NpcStateController>();
                stateController.SetDeadState();
                stateController.RagdollController.AddForceToFoot(stateController.transform.forward + Vector3.up * _forceToApply);
                AudioManager.instance.Play3DOneShot(_slatSound, gameObject);
                Destroy(gameObject);
            }
        }
    }
}