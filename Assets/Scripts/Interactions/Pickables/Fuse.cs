using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fuse : MonoBehaviour, IPickable
{
    private Rigidbody body;

    private void Awake()
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

    public void SetFuseBox(Transform fusePosition, FuseBox box)
    {
        Player.Instance.ReleasePickup();

        transform.SetParent(fusePosition);

        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        body.isKinematic = true;

        gameObject.layer = 0;
        box.gameObject.layer = 0;
    }
}
