using System;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

namespace Feature.NPC.Scripts
{
    public class NPCOnDeathScript : MonoBehaviour
    {
        [SerializeField] private float _force = 10f;
        [SerializeField] private float _timeToDeleteAfterFlying = 10f;
        [SerializeField] private GameObject _light;

        private NpcStateController _npcStateController;

        private bool _isDead = false;
        private float _timer;
        private float _secondTimer;

        private bool _lightActivated = false;
        private GameObject _lightObj;
        private Light _lightComp;

        private float _ogInstensity;
        private float _ogSpotAngle;
        private void Awake()
        {
            _npcStateController = GetComponent<NpcStateController>();
            _npcStateController.OnNpcDeath += OnNpcDeath;
        }

        private void OnDisable()
        {
            _npcStateController.OnNpcDeath -= OnNpcDeath;
        }

        private void OnNpcDeath()
        {
            _isDead = true;
            NPCSpawner.NPCCounter--;
            NPCDeathCounterUI.NPCDeathCounter++;
            AudioManager.instance.Play3DOneShot("event:/NPC Death", _npcStateController.transform.position);
        }

        private void Update()
        {
            if (!_isDead)
                return;

            _timer += Time.deltaTime;

            if (_timer > _npcStateController.Settings.TimeToGoToHeaven)
            {
                if (!_lightActivated)
                {
                    _lightObj = Instantiate(_light, transform.position, Quaternion.identity);
                    _lightComp = _lightObj.GetComponentInChildren<Light>();
                    _lightActivated = true;
                    _ogInstensity = _lightComp.intensity;
                    _ogSpotAngle = _lightComp.spotAngle;
                    AudioManager.instance.Play3DOneShot("event:/AngelDeathChoir", _lightComp.transform.position);
                }
                
                _secondTimer += Time.deltaTime;
                // Reduce light intensity as we get closer to _timeToDeleteAfterFlying
                _lightComp.intensity = Mathf.Lerp(_ogInstensity, 0, _secondTimer / _timeToDeleteAfterFlying);
                
                // same with outer angle
                _lightComp.spotAngle = Mathf.Lerp(_ogSpotAngle, 0, _secondTimer / _timeToDeleteAfterFlying);
                
            }

            if (_secondTimer >= _timeToDeleteAfterFlying)
            {
                Destroy(_lightObj);
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            if (_timer < _npcStateController.Settings.TimeToGoToHeaven)
                return;
            _npcStateController.RagdollController.AddForceToOnDeathRigidbodies(Vector3.up * _force);
        }
    }
}