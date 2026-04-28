using System;
using UnityEngine;

public class FuseBox : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform fusePosition;

    public bool HasFuse { get; private set; } = false;

    public void OnInteract()
    { 
        if(Player.Instance.PickableSlot is Fuse fuse) 
        {
            fuse.SetFuseBox(fusePosition, this);
            HasFuse = true;
        }
    }
}
