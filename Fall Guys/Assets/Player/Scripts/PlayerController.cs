using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public GameController gameController;
    public TextMeshProUGUI playerHpText;
    public float playerSpeed;
    public float playerJumpForce;
    public float sensitivity;
    public float cameraSpeed;
    public float cameraDistance;
    public int hp;
    public int maxHealth = 100;
    public bool isGrounded = true;
    public bool isMoving = false;

    private Animator animator;
    private PlayerInput playerInput;
    private InputAction move;
    private Rigidbody rb;
    private float rotationX;
    private const float ANIMATOR_SMOOTHING = 5f;
    private const int CAMERA_HEIGHT = 2;

    //решил использовать InputSystem так как она достаточно удобная и ее легко можно изменить
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions.FindAction("Move");
        hp = maxHealth;
    }

    //использую FixedUpdate для того чтобы управление персонажем не зависило от фпс
    private void FixedUpdate()
    {
        PlayerMove();
        Jump();
        CameraMove();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            playerHpText.text = "0";
            gameController.Lose();
        }
        else
        {
            playerHpText.text = hp.ToString();
        }
    }

    private void PlayerMove()
    {
        Vector3 movement = move.ReadValue<Vector2>();
        isMoving = movement != Vector3.zero;
        UpdateAnimatorAxis("MoveX", movement.x);
        UpdateAnimatorAxis("MoveY", movement.y);
        Vector3 movementDirection = playerCamera.transform.forward * movement.y + playerCamera.transform.right * movement.x;
        rb.velocity = new Vector3(movementDirection.x * playerSpeed, rb.velocity.y, movementDirection.z * playerSpeed);
    }

    private void Jump()
    {
        if (!isGrounded) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isMoving)
            {
                animator.CrossFade("RunJump", 0f);
            }
            else
            {
                animator.CrossFade("IdleJump", 0f);
            }
            rb.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void CameraMove()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, transform.position + Quaternion.Euler(0, rotationX, 0) * new Vector3(0, CAMERA_HEIGHT, -cameraDistance), Time.deltaTime * cameraSpeed);
        playerCamera.transform.rotation = Quaternion.Euler(0, rotationX, 0) * Quaternion.LookRotation(transform.position - playerCamera.transform.position);
        playerCamera.transform.LookAt(transform);
        transform.rotation = Quaternion.Euler(0, rotationX, 0);
    }

    private void UpdateAnimatorAxis(string axis, float value)
    {
        animator.SetFloat(axis, value, ANIMATOR_SMOOTHING, ANIMATOR_SMOOTHING);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}