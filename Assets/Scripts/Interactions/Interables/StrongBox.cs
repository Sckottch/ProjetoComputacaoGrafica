using System;
using UnityEngine;

public class StrongBox : MonoBehaviour, IInteractable
{
    [SerializeField] private Door door;
    [SerializeField] private Transform flaskPosition;

    public void OnInteract()
    {
        if (Player.Instance.PickableSlot is not Flask flask) return;

        if (flask.IsRadioactive())
        {
            flask.SetBox(flaskPosition);

            flask.gameObject.layer = 0;
            gameObject.layer = 0;

            door.UnlockDoor();
            GameManager.Instance.Puzzle3Completed();
            return;
        }

        flask.SetBox(flaskPosition);
    }
}
