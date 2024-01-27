using System;
using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace
{
    public class BananaProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        
        private Rigidbody _rb;

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
    }
}