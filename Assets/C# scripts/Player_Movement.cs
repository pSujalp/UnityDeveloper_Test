using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float verticalSpeed = 1.0f;
    [SerializeField] private float jumpForce = 10.0f;

    private Rigidbody rb;
    private bool resetJump = false;

    private static readonly int Moving = Animator.StringToHash("x");
    private static readonly int Jump = Animator.StringToHash("jump");

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.useGravity = false; 
    }

    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * horizontalInput + transform.forward * verticalInput).normalized;
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);

        float moveInput = (horizontalInput != 0 || verticalInput != 0) ? 1f : 0f;
        animator.SetFloat(Moving, Mathf.Lerp(animator.GetFloat(Moving), moveInput, Time.deltaTime * 4.5f));

        
        if (Input.GetButtonDown("Jump") && !resetJump)
        {
            rb.linearVelocity += -Physics.gravity.normalized * jumpForce;
            resetJump = true;
            animator.SetBool(Jump, true);
        }

        

        
        // float mouseX = Input.GetAxis("Mouse X");
        // transform.Rotate(Vector3.up, mouseX);
    }

    void FixedUpdate()
    {
        
        rb.AddForce(Physics.gravity, ForceMode.Acceleration);

        
        AlignWithGravity();
    }

    private void AlignWithGravity()
    {
        
        Vector3 gravityUp = -Physics.gravity.normalized;
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, gravityUp);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 5f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default") || collision.gameObject.CompareTag("Ground"))
        {
            resetJump = false;
            animator.SetBool(Jump, false);
        }
    }
}
