using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    public IPickable PickableSlot { get; private set; }

    public void ReleasePickup()
    {
        PickableSlot?.OnRelease();
        PickableSlot = null;
    }

    public void Pickup(IPickable pickable)
    {
        PickableSlot = pickable;
    }
}