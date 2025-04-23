using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10.0f;
    public float strafeSpeed = 7.5f;
    public float moveAccel = 5.0f;

    private Vector3 movement;

    private Rigidbody rb;

    [Header("Jumping")]
    public float jumpPower = 7.0f;
    public bool isGrounded;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = GroundCheck();

        if (isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3.Normalize(movement);

        movement = (transform.forward * vert * moveSpeed) + (transform.right * horz * moveSpeed);


        rb.MovePosition(transform.position + movement * Time.deltaTime);

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, groundLayer))
        {
            return true;
        }

        return false;
    }
}
