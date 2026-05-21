using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StrongBox : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Door door;
    [SerializeField] private Transform flaskPosition;
    [SerializeField] private Transform playerPickupPosition;
    [SerializeField] private Transform doorTransform;

    [Space(10)]
    [Header("Configuração da Animação")]
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Vector3 animationOffset;

    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private bool hasFlask = false;
    private Flask currentFlask;

    private void Start()
    {
        initialPosition = doorTransform.localPosition;
        finalPosition = initialPosition + animationOffset;
    }

    public void OnInteract()
    {
        if (hasFlask && Player.Instance.PickableSlot == null)
        {
            currentFlask.OnPickup(playerPickupPosition);
            hasFlask = false;

            return;
        }

        if (hasFlask) return;

        if (Player.Instance.PickableSlot is not Flask flask) return;

        if (flask.IsRadioactive())
        {
            flask.SetBox(flaskPosition);
            currentFlask = flask;
            hasFlask = true;

            flask.gameObject.layer = 0;
            gameObject.layer = 0;

            StartCoroutine(CloseDoorAnimation());

            door.UnlockDoor();
            GameManager.Instance.Puzzle3Completed();
            return;
        }

        flask.SetBox(flaskPosition);
        currentFlask = flask;
        hasFlask = true;
    }

    private IEnumerator CloseDoorAnimation()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            doorTransform.localPosition = Vector3.Lerp(initialPosition, finalPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorTransform.localPosition = finalPosition;
    }
}
