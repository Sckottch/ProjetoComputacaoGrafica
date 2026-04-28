using UnityEditor.UIElements;
using UnityEngine;


public class Fuse : PickableObject
{
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
