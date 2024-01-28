using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonkeyPooper : MonoBehaviour
{
    [SerializeField] private GameObject _poopPrefab;
    [SerializeField] private Transform _poopOrigin;
    [SerializeField] private EventReference _poopSound;

    private float _lastPoopTime;
    private float _nextPoopTime;

    private void Awake()
    {
        _nextPoopTime = Random.Range(10, 60);
    }

    private void Update()
    {
        if (Time.time - _lastPoopTime > _nextPoopTime)
        {
            _lastPoopTime = Time.time;
            _nextPoopTime = Random.Range(10, 60);
            SpawnPoop();
        }
    }

    private void SpawnPoop()
    {
        Instantiate(_poopPrefab, _poopOrigin.position, Quaternion.identity);
        AudioManager.instance.Play3DOneShot(_poopSound, gameObject);
    }

    [Button("Spawn poop")]
    private void Poo () =>  SpawnPoop();
}
