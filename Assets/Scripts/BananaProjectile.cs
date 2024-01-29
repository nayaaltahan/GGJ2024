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
        
        [SerializeField] private GameObject _bananaPeelPrefab;
        
        private float _timeAlive = 0f;
        private Rigidbody _rb;
        
        List<Transform> _objectsHit = new List<Transform>(); 

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        public void Shoot(Vector3 direction, float intensity)
        {
            transform.SetParent(null);
            _rb.isKinematic = false;
            _rb.AddForce(direction * _speed * intensity, ForceMode.Impulse);
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
            if (other.gameObject.CompareTag("NPCHitCollider") && _objectsHit.Contains(other.transform.parent) == false)
            {
                NPCDeathCounterUI.NPCDeathCounter++;
                _objectsHit.Add(other.transform.parent);
                other.gameObject.GetComponentInParent<NpcStateController>()?.SetState(NpcState.Chase);
                AudioManager.instance.Play3DOneShot("event:/NPC Aggro", other.gameObject.transform.position);
            }
            else if (other.gameObject.CompareTag("Ground"))
            {
                var peel = Instantiate(_bananaPeelPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                // Peel VFX as well and sound
                Destroy(gameObject);                
            }
            AudioManager.instance.Play3DOneShot("event:/SFX/Attacks/banana_hit_throw", transform.position);
        }
    }
}