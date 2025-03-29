using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float rotationSpeed = 2.0f; 

    [SerializeField] float aligningSpeed = 6.5f; 
    [SerializeField] GameObject camera;


    private Rigidbody rb;
    private bool resetJump = false;

    private static readonly int Moving = Animator.StringToHash("x");
    private static readonly int Jump = Animator.StringToHash("jump");

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
       
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(Vector3.up, mouseX * rotationSpeed);
        Vector3 currentRotation = camera.transform.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360; 
        float newRotationX = Mathf.Clamp(currentRotation.x - mouseY * rotationSpeed, -19.5f, 25f);
        camera.transform.localEulerAngles = new Vector3(newRotationX, currentRotation.y, currentRotation.z);

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
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * aligningSpeed));
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
