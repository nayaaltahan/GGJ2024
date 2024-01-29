using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Feature.Ragdoll;
using RengeGames.HealthBars;
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
        [SerializeField] private float _maxHoldTime = .5f;
        [SerializeField] private RadialSegmentedHealthBar _healthBar;

        private int _currentAmmo;
        private int _maxAmmo;


        private float _holdTime;
        private bool _isHoldingDown = false;
        private bool _isOnCooldown;

        private Animator _animator;

        private RagdollController _ragdollController;
        private Transform _camTransform;
        private int _ammo = 0;
        private static readonly int TriggerShoot = Animator.StringToHash("Trigger_Shoot");

        private void Awake()
        {
            _camTransform = Camera.main.transform;
            _animator = GetComponentInChildren<Animator>();
            _ragdollController = GetComponent<RagdollController>();
            _healthBar.GetComponent<CanvasGroup>().alpha = 0;
            _healthBar.SetPercent(0);
            _maxAmmo = _bananaAmmoBelt.Count;
            
            for (var i = _currentAmmo; i < _maxAmmo; i++)
            {
                _bananaAmmoBelt[i].SetActive(false);
            }
        }

        private void Update()
        {
            if (_ragdollController.IsRagdoolActive)
                return;
            
            if (Input.GetButtonDown("Fire1"))
            {
                if (_currentAmmo <= 0)
                    return;
                
                _isHoldingDown = true;
                _holdTime = 0;
                _healthBar.GetComponent<CanvasGroup>().alpha = 1;
            }
            
            _holdTime += Time.deltaTime;
            var currentValue = Mathf.Clamp(_holdTime / _maxHoldTime, 0, 1);
            _healthBar.SetPercent(currentValue);
            
            if (Input.GetButtonUp("Fire1"))
            {
                if (_currentAmmo <= 0)
                    return;
                
                Shoot(currentValue);
                StartCooldown();
                _healthBar.GetComponent<CanvasGroup>().alpha = 0;
                _healthBar.SetPercent(0);
            }
        }

        public bool PickupAmmo()
        {
            if (_currentAmmo >= _maxAmmo)
                return false;
            _bananaAmmoBelt[_currentAmmo].SetActive(true);
            _currentAmmo++;
            return true;
        }

        private async void StartCooldown()
        {
            if (_isOnCooldown || _currentAmmo <= 0)
                return;
            _isOnCooldown = true;
            await UniTask.Delay(_coolDown);
            _isOnCooldown = false;
        }

        private async void Shoot(float holdTime)
        {
            if (_isOnCooldown || _currentAmmo <= 0)
                return;
            
            _currentAmmo--;
            _bananaAmmoBelt[_currentAmmo].SetActive(false);
            
            AudioManager.instance.Play3DOneShot("event:/SFX/Attacks/Banana_Throw", transform.position);
            var projectile = Instantiate(_bananaPrefab, _shootFrom.position, _shootFrom.rotation, _shootFrom);
            _animator.SetTrigger(TriggerShoot);
            await UniTask.Delay(_delayBeforeShoot);
            projectile.transform.SetParent(null);
            // projectile.GetComponent<BananaProjectile>().Shoot(_camTransform.forward, holdTime);
            // Shoot from the _shootFrom location towards the middle of the screen
            // projectile.GetComponent<BananaProjectile>().Shoot(ray.direction , holdTime);
            // Set direction from the _shootFrom to the middle of the screen
            int x = Screen.width / 2;
            int y = Screen.height / 2;
 
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            projectile.GetComponent<BananaProjectile>().Shoot(ray.direction , holdTime);
        }
    }
}