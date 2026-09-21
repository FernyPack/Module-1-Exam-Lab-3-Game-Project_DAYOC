using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public AudioSource movementAudio;
    public AudioSource sprintAudio;
    public AudioSource jumpAudio;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (jumpAudio != null)
                jumpAudio.Play();

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        bool isMoving = Input.GetAxisRaw("Horizontal") != 0 ||
                Input.GetAxisRaw("Vertical") != 0;

        if (isMoving)
        {
            if (!movementAudio.isPlaying) movementAudio.Play();
        }
        else
        {
            movementAudio.Stop();
        }

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && isMoving;
        if (isSprinting)
        {
            if (!sprintAudio.isPlaying)
                sprintAudio.Play();
        }
        else
        {
            if (sprintAudio.isPlaying)
                sprintAudio.Stop();
        }
    }
}