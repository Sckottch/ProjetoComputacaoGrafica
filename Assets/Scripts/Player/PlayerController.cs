using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private PlayerControlsInputs inputActions;
    private Vector2 moveInput;
    private Rigidbody body;

    [Header("Configarações")]
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        inputActions = new();

        inputActions.Gameplay.Move.performed += OnFirstInput;
    }

    private void OnFirstInput(InputAction.CallbackContext context)
    {
        inputActions.Gameplay.Move.performed -= OnFirstInput;
        GameManager.Instance.GameStarted();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Enable();

        inputActions.Gameplay.Release.started += OnReleaseInput;

        inputActions.Gameplay.Move.performed += OnMoveInputPerformed;
        inputActions.Gameplay.Move.canceled += OnMoveInputCanceled;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();

        inputActions.Gameplay.Release.started -= OnReleaseInput;
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = Camera.main.transform.right * moveInput.x + Camera.main.transform.forward * moveInput.y;

        body.linearVelocity = new Vector3(moveDir.x * moveSpeed, body.linearVelocity.y, moveDir.z * moveSpeed);
    }

    private void OnReleaseInput(InputAction.CallbackContext context)
    {
        Player.Instance.ReleasePickup();
    }

    private void OnMoveInputPerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveInputCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
}
