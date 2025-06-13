using UnityEngine;
using Mirror; // Important

public class TomatoController : NetworkBehaviour
{
    public float rollSpeed = 10f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;  // Only control the local player

        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jumpPressed = jumpPressed || Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        Vector3 force = new Vector3(moveInput.x, 0, moveInput.y) * rollSpeed;
        rb.AddTorque(transform.right * -force.z + transform.forward * force.x);

        if (jumpPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = false;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
