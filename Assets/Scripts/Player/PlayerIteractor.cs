using UnityEngine;
using UnityEngine.UI;

public class PlayerIteractor : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Image reticleImage;
    [SerializeField] private Transform pickupPosition;

    private Color originalReticleColor;
    private PlayerControlsInputs controls;

    private IInteractable currentTarget;
    private IPickable currentPickupTarget;

    private void Awake()
    {
        controls = new();


        originalReticleColor = reticleImage.color;
        controls.Gameplay.Interact.started += ctx => TryInteract();
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
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.TryGetComponent(out IInteractable interactable))
            {
                if(currentTarget != interactable)
                {
                    currentTarget = interactable;
                    reticleImage.color = Color.green; 
                }

                return;
            }

            if (hitObj.TryGetComponent(out IPickable pickable) && Player.Instance.PickableSlot == null)
            {
                if(currentPickupTarget != pickable)
                {
                    currentPickupTarget = pickable;
                    reticleImage.color = Color.yellow; 
                }
    
                return;
            }
        }

        if (currentTarget != null || currentPickupTarget != null)
        {
            ClearTargets();
        }
    }

    private void ClearTargets()
    {
        currentTarget = null;
        currentPickupTarget = null;
        reticleImage.color = originalReticleColor;
    }

    private void TryInteract()
    {
        currentTarget?.OnInteract();

        currentPickupTarget?.OnPickup(pickupPosition);
    }
}

public interface IInteractable
{
    void OnInteract();
}

public interface IPickable
{
    void OnPickup(Transform pickupPosition);

    void OnRelease();
}