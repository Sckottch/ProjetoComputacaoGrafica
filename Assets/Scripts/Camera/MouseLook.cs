using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        Vector2 mouseInput = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseInput.x);
    }
}
