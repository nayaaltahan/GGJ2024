using System;
using System.Collections.Generic;
using Feature.NPC.Scripts;
using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace
{
    public class BananaProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        [SerializeField] private float _timeToDespawsn = 20f;
        
        private float _timeAlive = 0f;
        private Rigidbody _rb;
        
        List<Transform> _objectsHit = new List<Transform>(); 

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        public void Shoot(Vector3 direction)
        {
            transform.SetParent(null);
            _rb.isKinematic = false;
            _rb.AddForce(direction * _speed, ForceMode.Impulse);
        }

        private void Update()
        {
            _timeAlive += Time.deltaTime;
            if (_timeAlive > _timeToDespawsn)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("NPCHitCollider") && _objectsHit.Contains(other.transform.transform) == false)
            {
                _objectsHit.Add(other.transform.parent);
                other.gameObject.GetComponentInParent<NpcStateController>()?.SetState(NpcState.Chase);
            }
        }
    }
}