using UnityEngine;

public class TESTCONTROLLER : MonoBehaviour
{

    [SerializeField] CharacterController body;
    [SerializeField] float speed = 15f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float gravity = -20f;
    [SerializeField] float maxHopSpeed = 90f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float playerHeight = 5.0f;
    float lastGroundedTime;
    int consecutiveJumps;
    float defaultMaxSpeed;
    Vector3 move;
    Vector3 vel;

    public float maxAirWishSpeed = 20f;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultMaxSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Move();
    }

    void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f, groundMask);
        if (isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        Vector3.Normalize(move);

        move = (transform.forward * vert * speed) + (transform.right * horz * speed);

        move = move + vel * Time.fixedDeltaTime;

        body.Move(transform.position + move * Time.fixedDeltaTime);
        
    }

    void AirAccel(Vector3 wishDir, float wishSpeed, float airAccelerate)
    {
        float addSpeed;
        float accelSpeed;
        float currSpeed;

        if (wishSpeed > maxAirWishSpeed)
        {
            wishSpeed = maxAirWishSpeed;
        }

        currSpeed = Vector3.Dot(vel, wishDir);

        addSpeed = wishSpeed - currSpeed;

        if (addSpeed <= 0.0f)
        {
            return;
        }

        accelSpeed = airAccelerate * Time.fixedDeltaTime * wishSpeed;

        if (accelSpeed > addSpeed)
        {
            accelSpeed = addSpeed;
        }

        vel += accelSpeed * wishDir;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.Move(new Vector3(0f, Vector3.up.y * jumpForce, 0f));
        }
    }
    void Move()
    {
        
    }
}
