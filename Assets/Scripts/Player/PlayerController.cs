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

        inputActions.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Gameplay.Release.started += ctx => Player.Instance.ReleasePickup();
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = transform.right * moveInput.x + transform.forward * moveInput.y;

        body.linearVelocity = new Vector3(moveDir.x * moveSpeed, body.linearVelocity.y, moveDir.z * moveSpeed);
    }
}
