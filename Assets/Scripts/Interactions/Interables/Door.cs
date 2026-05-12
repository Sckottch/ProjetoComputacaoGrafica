using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Referencias")]
    [SerializeField] private MeshRenderer doorLamp;
    [SerializeField] private Material unlockedMaterial;

    [Space(10)]
    [Header("Configurações de Animação")]
    [SerializeField] private Vector3 displacement = new(3f, 0f, 0f);
    [SerializeField] private float duration = 1f;

    private Vector3 initialPosition;
    private Vector3 finalPosition;

    private bool isLocked = true;

    private void Start()
    {
        initialPosition = transform.localPosition;
        finalPosition = initialPosition + displacement;
    }

    public void OnInteract()
    {
        if (isLocked) return;

        gameObject.layer = 0;
        StartCoroutine(OpenAnimation());
    }

    public void UnlockDoor()
    {
        isLocked = false;

        gameObject.layer = 6;

        doorLamp.material = unlockedMaterial;
    }

    private IEnumerator OpenAnimation()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = finalPosition;
    }
}
