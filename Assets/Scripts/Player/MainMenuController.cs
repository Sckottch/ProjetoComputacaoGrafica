using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    private PlayerControlsInputs inputActions;

    private void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Enable();

        inputActions.Gameplay.Interact.started += LoadGameScene;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();

        inputActions.Gameplay.Interact.started -= LoadGameScene;
    }

    private void LoadGameScene(InputAction.CallbackContext context)
    {
        GameManager.Instance.LoadGame();
    }
}
