using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonkeyPooper : MonoBehaviour
{
    [SerializeField] private GameObject _poopPrefab;
    [SerializeField] private Transform _poopOrigin;

    private float _lastPoopTime;
    private float _nextPoopTime;

    private void Awake()
    {
        _nextPoopTime = Random.Range(10, 180);
    }

    private void Update()
    {
        if (Time.time - _lastPoopTime > _nextPoopTime)
        {
            _lastPoopTime = Time.time;
            _nextPoopTime = Random.Range(10, 180);
            Instantiate(_poopPrefab, _poopOrigin.position, Quaternion.identity);
        }
    }
    
    [Button("Spawn poop")]
    private void Poo () =>  Instantiate(_poopPrefab, -transform.forward + _poopOrigin.position, Quaternion.identity);
}
