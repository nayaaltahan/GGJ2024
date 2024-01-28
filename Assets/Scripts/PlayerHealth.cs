using System;
using Cysharp.Threading.Tasks;
using Feature.Ragdoll;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private int _timeToStandUp = 2000;
        [SerializeField] private Transform _graphics;
        
        private float _currentHealth;
        private bool _isKnockedOut = false;
        
        private RagdollController _ragdollController;
        
        
        private void Awake()
        {
            _ragdollController = GetComponent<RagdollController>();
        }

        public void TakeDamage(float damage, Vector3 force = default)
        {
            if (_isKnockedOut)
                return;
            
            _currentHealth -= damage;
            // maybe always get knocked out
            if (_currentHealth <= 0)
            {
                _isKnockedOut = true;
                _ragdollController.ActivateRagdoll();
                // await UniTask.DelayFrame(1);
                _ragdollController.AddForceToHead(force);
                // await UniTask.Delay(_timeToStandUp);
                // transform.position = _graphics.transform.position;
                // transform.position = Vector3.up * 0.5f;
                // _ragdollController.DeactivateRagdoll();
                // _isKnockedOut = false;
            }
        }

        private void Update()
        {
            if (_isKnockedOut)
                transform.position = _graphics.transform.position;
        }
    }
}