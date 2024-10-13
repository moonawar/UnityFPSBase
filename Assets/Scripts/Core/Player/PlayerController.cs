using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerStats stats;
    private Vector3 velocity;
    [SerializeField] private Animator animator;

    [Header("Ground Check")]
    [SerializeField] private Transform gcOrigin;
    [SerializeField] private float gcDistance = 0.4f;
    private LayerMask groundLayer;
    private bool isGrounded = true;
    private bool startFlag = false;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void OnEnable() {
        if (!startFlag) return;
        InputContainer.Instance.Controls.Gameplay.Jump.performed += Jump;
    }

    private void Start() {
        InputContainer.Instance.Controls.Gameplay.Jump.performed += Jump;
        startFlag = true;
    }

    private void OnDisable() {
        InputContainer.Instance.Controls.Gameplay.Jump.performed -= Jump;
    }

    private void Jump(InputAction.CallbackContext _) {
        if (isGrounded) {
            velocity.y = Mathf.Sqrt(stats.JumpHeight * -2f * Constant.GRAVITY);
        }
    }

    private void Update() {
        isGrounded = Physics.CheckSphere(gcOrigin.position, gcDistance, groundLayer);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        Vector2 input = InputContainer.Instance.Controls.Gameplay.Move.ReadValue<Vector2>();
        float x = input.x;
        float z = input.y;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(stats.BaseSpeed * Time.deltaTime * move);

        velocity.y += Constant.GRAVITY * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        animator.SetFloat("Speed", move.sqrMagnitude);
    }
}