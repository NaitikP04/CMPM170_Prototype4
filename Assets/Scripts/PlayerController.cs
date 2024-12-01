using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 9f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public enum PlatformType { Normal, jumpPad }
    public PlatformType equippedPlatformType = PlatformType.Normal;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
        ChangePlatformType();
    }

    void Move()
    {
        float moveHorizontal = -(Input.GetAxis("Horizontal"));
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ChangePlatformType()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            equippedPlatformType = PlatformType.Normal;
            Debug.Log("Equipped Normal Platform");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            equippedPlatformType = PlatformType.jumpPad;
            Debug.Log("Equipped JumpPad");
        }
    }
}
