using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Feature.Ragdoll
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private GameObject _environmentColliderParent;
        [SerializeField] private bool _startActive = false;
        
        [FoldoutGroup("Ragdoll references")]
        [SerializeField]
        private List<Collider> _ragdollColliders;
        
        [FoldoutGroup("Ragdoll references")]
        [SerializeField]
        private List<Rigidbody> _ragdollRigidBodies;
        
        
#if UNITY_EDITOR  
        [FoldoutGroup("Ragdoll references")]
        [Button("Get Ragdoll Parts")]
        private void SetRagdollParts()
        {
            Debug.Log("Getting ragdoll parts", gameObject);
            _ragdollColliders = new List<Collider>();
            _ragdollRigidBodies = new List<Rigidbody>();
            
            // Get all ragdoll parts
            var ragdollParts = GetComponentsInChildren<RagdollPart>();
            
            // Loop through all ragdoll parts
            foreach (var ragdollPart in ragdollParts)
            {
                // Get the collider and rigidbody
                var collider = ragdollPart.GetComponent<Collider>();
                var rigidbody = ragdollPart.GetComponent<Rigidbody>();
                
                // Add them to the lists
                _ragdollColliders.Add(collider);
                _ragdollRigidBodies.Add(rigidbody);
            }
        }
#endif

        private void Awake()
        {
            if(_startActive)
                ActivateRagdoll();
            else
                DeactivateRagdoll();
        }

        public void ActivateRagdoll()
        {
            _environmentColliderParent.SetActive(false);
            for (var i = 0; i < _ragdollColliders.Count; i++)
            {
                _ragdollColliders[i].enabled = true;
                _ragdollRigidBodies[i].isKinematic = false;
            }
        }

        public void DeactivateRagdoll()
        {
            _environmentColliderParent.SetActive(true);
            for (var i = 0; i < _ragdollColliders.Count; i++)
            {
                _ragdollColliders[i].enabled = false;
                _ragdollRigidBodies[i].isKinematic = true;
            }
        }
        
        public void AddForceToRagdoll(Vector3 force)
        {
            foreach (var rigidbody in _ragdollRigidBodies)
            {
                rigidbody.AddForce(force);
            }
        }
    }
}