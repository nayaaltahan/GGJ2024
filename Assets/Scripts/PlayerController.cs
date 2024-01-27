using System;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public PlayerModel playerSettings;
    private Rigidbody rigidbody;
    private float _timeNotGrounded = 0f;
    private float _timeJumping = 0f;
    private bool _jumped;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & IsGrounded())
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
            _timeJumping = 0f;
            _jumped = true;
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (!IsGrounded())
        {
            _timeNotGrounded += Time.deltaTime;
            rigidbody.AddForce(0, playerSettings.gravity * playerSettings.GravityCurve.Evaluate(_timeNotGrounded / playerSettings.TimeToFullGravity), 0, ForceMode.Force);
        }
        else
        {
            _timeNotGrounded = 0;
        }

        if (_jumped)
        {
            _timeJumping += Time.deltaTime;
            if (_timeJumping >= playerSettings.AirTime)
            {
                _jumped = false;
            }
        }
        var yVelocity = playerSettings.jumpForce * playerSettings.JumpCurve.Evaluate(_timeJumping / playerSettings.AirTime);
        movement.y += yVelocity * Time.deltaTime;

        rigidbody.velocity = movement;
    }

    private bool IsGrounded()
    {
        // Check if the player is on the ground
        return Physics.CheckSphere(transform.position, playerSettings.groundCheckDistance, playerSettings.groundLayer);
    }

    private void OnDrawGizmos()
    {
        // Draw a sphere to show the ground check distance
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, playerSettings.groundCheckDistance);
    }
}