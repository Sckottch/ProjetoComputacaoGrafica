using System;
using UnityEngine;

public class FuseBox : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform fusePosition;

    public void OnInteract()
    { 
        if(Player.Instance.PickableSlot is Fuse fuse) 
        {
            fuse.SetFuseBox(fusePosition, this);
        }
    }
}
