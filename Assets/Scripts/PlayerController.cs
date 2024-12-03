using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float OGmoveSpeed = 8f;
    public float moveSpeed;
    public float jumpForce = 9f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public enum PlatformType { Normal, jumpPad, speedBoost }
    public PlatformType equippedPlatformType = PlatformType.Normal;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = OGmoveSpeed;
    }

    void Update()
    {
        CheckGround();
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
        isGrounded = CheckGround();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded)
        {
            // Use Physics.OverlapSphere to detect all colliders in the area
            Collider[] colliders = Physics.OverlapSphere(groundCheck.position, 0.1f, groundLayer);

            bool touchingSpeedBoost = false;

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("SpeedBoost"))
                {
                    touchingSpeedBoost = true;
                    break;
                }
            }

            if (!touchingSpeedBoost)
            {
                moveSpeed = OGmoveSpeed;
            }
        }

        return isGrounded;
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

        else if (Input.GetKeyDown(KeyCode.R))
        {
            equippedPlatformType = PlatformType.speedBoost;
            Debug.Log("Equipped SpeedBoost");
        }
    }
}
