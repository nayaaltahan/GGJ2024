using System;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _modelTransform;

    public PlayerModel playerSettings;
    private Rigidbody rigidbody;
    private Camera _camera;
    private Animator _animator;
    private float _timeNotGrounded = 0f;
    private float _timeJumping = 0f;
    private bool _jumped;
    private static readonly int MoveVelocity = Animator.StringToHash("MoveVelocity");
    private Vector3 movement;
    private Vector2 moveDirection;
    private bool _isSprinting = false;
    private static readonly int TriggerJumping = Animator.StringToHash("Trigger_Jump");
    private static readonly int ExitJump = Animator.StringToHash("Trigger_ExitJump");
    private static readonly int TimeFalling = Animator.StringToHash("TimeFalling");


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
        
        // TODO: Delete
        Cursor.lockState = CursorLockMode.Locked;
        // hide cursor
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & IsGrounded())
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
            _animator.SetTrigger(TriggerJumping);
            _timeJumping = 0f;
            _jumped = true;
        }

        _isSprinting = IsGrounded() && Input.GetKey(KeyCode.LeftShift);


        // set animator running speed
        var direction = new Vector3(movement.x, 0, movement.z);
        var moveMagnitude = direction.magnitude;
        if (_isSprinting)
        {
            // Lerp moveMagnitude from current value to 2
            moveMagnitude = Mathf.Lerp(moveMagnitude, 2f, Time.deltaTime * 10f);
        }
        else
        {
            moveMagnitude = Mathf.Clamp(moveMagnitude, 0f, 1f);
        }

        _animator.SetFloat(MoveVelocity, moveMagnitude);
        _animator.SetFloat(TimeFalling, _timeNotGrounded);

        // Rotate the model to the direction of movement
        if (moveMagnitude > 0.1f)
        {
            var rotation = Quaternion.LookRotation(direction);
            _modelTransform.rotation = Quaternion.Slerp(_modelTransform.rotation, rotation, Time.deltaTime * 10f);
        }
    }

    void FixedUpdate()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        movement = -new Vector3(moveDirection.x, 0.0f, moveDirection.y) * playerSettings.speed;
        movement = -_camera.transform.TransformDirection(movement);
        movement.y = 0;


        if (_isSprinting)
            movement *= 2;

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
        if (_jumped == false)
            _timeJumping = 0;

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