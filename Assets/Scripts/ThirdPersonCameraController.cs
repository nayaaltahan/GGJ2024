using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ThirdPersonCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _minMaxLookAngle = 80f;

        private void Update()
        {
            // Rotate the _target according to mouse Input. The target is the FollowTarget of a cinemachine camera
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
            
            _target.Rotate(Vector3.up, mouseX);
            _target.Rotate(Vector3.right, -mouseY);
            
            // Clamp the rotation of the target
            var targetRotation = _target.rotation.eulerAngles;
            targetRotation.x = Mathf.Clamp(targetRotation.x, -_minMaxLookAngle, _minMaxLookAngle);
            _target.rotation = Quaternion.Euler(targetRotation);
        }
    }
}