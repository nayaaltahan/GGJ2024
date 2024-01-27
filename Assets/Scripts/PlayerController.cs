using ScriptableObjects;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public PlayerModel playerSettings;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (Input.GetKeyDown(KeyCode.Space) & IsGrounded())
        {
            rigidbody.AddForce(0,playerSettings.jumpForce,0, ForceMode.Impulse);
        }

        if (!IsGrounded())
        {
            rigidbody.AddForce(0, playerSettings.gravity,0 , ForceMode.Force);
        }
        
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
