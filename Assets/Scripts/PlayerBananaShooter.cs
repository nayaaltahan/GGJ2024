using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace
{
    public class PlayerBananaShooter : MonoBehaviour
    {

        [SerializeField] private Transform _shootFrom;
        [SerializeField] private GameObject _bananaPrefab;
        [SerializeField] private List<GameObject> _bananaAmmoBelt;
        [SerializeField] private int _delayBeforeShoot = 500;
        [SerializeField] private int _coolDown = 1000;

        private bool _isOnCooldown;

        private Animator _animator;

        private Transform _camTransform;
        private int _ammo = 0;
        private static readonly int TriggerShoot = Animator.StringToHash("Trigger_Shoot");

        private void Awake()
        {
            _camTransform = Camera.main.transform;
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                StartCooldown();
            }
        }

        private async void StartCooldown()
        {
            if (_isOnCooldown)
                return;
            _isOnCooldown = true;
            await UniTask.Delay(_coolDown);
            _isOnCooldown = false;
        }

        private async void Shoot()
        {
            if (_isOnCooldown)
                return;
            
            var projectile = Instantiate(_bananaPrefab, _shootFrom.position, Quaternion.identity, _shootFrom);
            _animator.SetTrigger(TriggerShoot);
            await UniTask.Delay(_delayBeforeShoot);
            projectile.GetComponent<BananaProjectile>().Shoot(_camTransform.forward);
            
        }
    }
}