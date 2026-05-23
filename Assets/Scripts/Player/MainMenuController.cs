using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private PlayerControlsInputs inputActions;

    private void Awake()
    {
        inputActions = new();

        inputActions.Gameplay.Interact.started += ctx => GameManager.Instance.LoadGame();
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();
    }
}
