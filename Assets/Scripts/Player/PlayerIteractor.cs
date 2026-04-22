using UnityEngine;
using UnityEngine.UI;

public class PlayerIteractor : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Image reticleImage;

    private Color originalReticleColor;
    private PlayerControlsInputs controls;
    private IInteractable currentTarget;

    private void Awake()
    {
        controls = new();
        controls.Gameplay.Interact.started += ctx => TryInteract();

        originalReticleColor = reticleImage.color;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        CheckGaze();
    }

    private void CheckGaze()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if(currentTarget != interactable)
                {
                    currentTarget = interactable;
                    reticleImage.color = Color.green; 
                }

                return;
            }
        }

        if (currentTarget != null)
        {
            currentTarget = null;
            reticleImage.color = originalReticleColor;
        }
    }

    private void TryInteract()
    {
        currentTarget?.OnInteract();
    }
}

public interface IInteractable
{
    void OnInteract();
}