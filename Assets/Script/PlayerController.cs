using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 8f;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.ResetTrigger("Jump");
        }

        float moveZ = Input.GetAxis("Vertical");
        if (moveZ != 0)
        {
            transform.rotation = Quaternion.Euler(0, moveZ > 0 ? 0 : 180, 0);
        }

        Vector3 move = transform.forward * Mathf.Abs(moveZ);
        controller.Move(move * moveSpeed * Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(moveZ));

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpForce;
            animator.SetTrigger("Jump");
        }
    }
}
