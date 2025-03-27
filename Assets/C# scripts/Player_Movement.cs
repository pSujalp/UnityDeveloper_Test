using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Animator animator;

    int moving = Animator.StringToHash("x");

    int jump = Animator.StringToHash("jump");

    [SerializeField] private float speed = 5.0f;

    private float moveInput;

    [SerializeField] private float verticalSpeed = 1.0f;

    bool resetJump=false;

    [SerializeField] private float jumpForce = 10.0f;
    void Start()
    {
        resetJump = false;
        animator = GetComponent<Animator>();
    }
   
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        moveInput = (horizontalInput != 0 || verticalInput != 0) ? 1f : 0f;

        float currentMove = animator.GetFloat(moving);
        animator.SetFloat(moving, Mathf.Lerp(currentMove, moveInput, Time.deltaTime * 4.5f));

        float X = verticalSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, X, 0);

        if(Input.GetButton("Jump") && resetJump==false)
        {
            
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            resetJump = true;
            animator.SetBool(jump, resetJump);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            resetJump = false;
            animator.SetBool(jump, resetJump);
        }
    }
}
