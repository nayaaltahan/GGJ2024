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
    [SerializeField] private Transform _graphics;


    private bool _isActive = true;
    private float _timePickedUp;

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    private void Update()
    {
        // float the graphics up and down
        if (_isActive)
        {
            var position = _graphics.transform.position;
            position = new Vector3(position.x, position.y + Mathf.Sin(Time.time) * 0.005f, position.z);
            _graphics.transform.position = position;
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