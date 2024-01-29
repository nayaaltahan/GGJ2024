using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

public class BananaAmmo : MonoBehaviour
{
    [SerializeField] private float _respawnTime = 10f;
    [SerializeField] private float _height = 2f;
    [SerializeField] private Transform _graphics;


    private bool _isActive = true;
    private float _timePickedUp;

    private Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = _graphics.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive == false)
            return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            var pickedUpAmmo = other.gameObject.GetComponentInParent<PlayerBananaShooter>().PickupAmmo();

            _isActive = !pickedUpAmmo;
            _graphics.gameObject.SetActive(!pickedUpAmmo);
            _timePickedUp = Time.time;
            AudioManager.instance.PlayOneShot("event:/pickup");
        }
    }

    private void Update()
    {
        // float the graphics up and down
        if (_isActive)
        {
            float mod = 0.5f + Mathf.Sin (Time.time) * 0.5f;
            _graphics.transform.position = _startPos + Vector3.up * mod * _height;
        }
        else
        {
            if (Time.time - _timePickedUp > _respawnTime)
            {
                _isActive = true;
                _graphics.gameObject.SetActive(true);
            }
        }
    }
}