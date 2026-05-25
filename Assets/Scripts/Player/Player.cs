using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : SingletonMonoBehaviour<Player>
{
    public IPickable PickableSlot { get; private set; }

    private PlayerController controller;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
    }

    public void ReleasePickup()
    {
        PickableSlot?.OnRelease();
        PickableSlot = null;
    }

    public void Pickup(IPickable pickable)
    {
        PickableSlot = pickable;
    }

    public void EnableControls()
    {
        controller.enabled = true;
    }
}