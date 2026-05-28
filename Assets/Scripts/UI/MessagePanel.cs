using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    [Header("Referencias da UI")]
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI subText;

    [Space(10)]
    [Header("Configurações")]
    [SerializeField] private string victoryTitle;
    [SerializeField] private string victorySubText;
    [SerializeField] private string defeatTitle;
    [SerializeField] private string defeatSubText;
    [SerializeField] private Color victoryColor;
    [SerializeField] private Color defeatColor;

    private PlayerControlsInputs inputActions;

    private void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameEnd += ShowMessage;

        inputActions.Gameplay.Interact.started += OnInteractInput;
        inputActions.Gameplay.Release.started += OnReleaseInput;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameEnd -= ShowMessage;

        inputActions.Gameplay.Disable();

        inputActions.Gameplay.Interact.started -= OnInteractInput;
        inputActions.Gameplay.Release.started -= OnReleaseInput;
    }

    private void ShowMessage(bool isVictory)
    {
        Player.Instance.DisableControls();
        inputActions.Gameplay.Enable();

        if (isVictory)
        {
            background.color = victoryColor;
            titleText.text = victoryTitle;
            subText.text = victorySubText;
        }
        else
        {
            background.color = defeatColor;
            titleText.text = defeatTitle;
            subText.text = defeatSubText;
        }

        background.gameObject.SetActive(true);
    }

    private void OnInteractInput(InputAction.CallbackContext context)
    {
        GameManager.Instance.LoadGame();
    }

    private void OnReleaseInput(InputAction.CallbackContext context)
    {
        GameManager.Instance.LoadMainMenu();
    }
}