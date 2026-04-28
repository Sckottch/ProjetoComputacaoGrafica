using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour, IPickable
{
    protected Rigidbody body;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void OnPickup(Transform pickupPosition)
    {
        transform.SetParent(pickupPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        body.isKinematic = true;
        Player.Instance.Pickup(this);
    }

    public void OnRelease()
    {
        transform.SetParent(null);

        body.isKinematic = false;
    }
}