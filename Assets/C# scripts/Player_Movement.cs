using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Animator animator;

    int moving = Animator.StringToHash("x");

    [SerializeField] private float speed = 5.0f;

    private float moveInput;

    [SerializeField] private float verticalSpeed = 1.0f;

    void Start()
    {
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
    }
}
