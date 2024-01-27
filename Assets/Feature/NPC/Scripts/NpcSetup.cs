using System;
using System.Collections.Generic;
using Feature.Ragdoll;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Feature.NPC.Scripts
{
    public class NpcSetup : MonoBehaviour
    {
        [SerializeField]
        private NpcModelList _modelList;
        [SerializeField]
        private Renderer _renderer;
        
        [SerializeField]
        private List<Collider> _ragdollColliders;
        
        [SerializeField]
        private List<Rigidbody> _ragdollRigidBodies;

        [SerializeField] private GameObject _environmentColliderParent;
        
        
        private void Awake()
        {
            // Set texture to a random one from the model list
            _renderer.material.mainTexture = _modelList.Textures[UnityEngine.Random.Range(0, _modelList.Textures.Count)];
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
        
#if UNITY_EDITOR  
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

            PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
        }
#endif
    }
}