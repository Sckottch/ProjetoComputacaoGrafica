using System.Collections;
using UnityEngine;

public class WireBoxButton : MonoBehaviour, IInteractable
{
    [Header("Configurações")]
    public WireColor wireColor;

    [Space(10)]
    [Header("Referências")]
    [SerializeField] private WireBox wireBox;
    [SerializeField] private GameObject lightObject;

    [Space(10)]
    [Header("Configurações de Animação")]
    [SerializeField] private Vector3 moveOffset;
    [SerializeField] private float duration;

    private Vector3 initialPosition;
    private Vector3 finalPosition;

    private bool isConnected = false;

    private void Start()
    {
        initialPosition = transform.localPosition;
        finalPosition = initialPosition + moveOffset;
    }

    public void OnInteract()
    {
        if (isConnected)
        {
            StartCoroutine(PressAnimation());
            return;
        }

        wireBox.ConnectWire(this);
        StartCoroutine(PressAnimation());
    }

    private IEnumerator PressAnimation()
    {
        gameObject.layer = 0; // Desativa a interação durante a animação

        float elapsedTime = 0f;
        float halfDuration = duration / 2f;

        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration;
            transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, t);

            yield return null;
        }

        transform.localPosition = finalPosition;
        elapsedTime = 0f;

        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration;
            transform.localPosition = Vector3.Lerp(finalPosition, initialPosition, t);

            yield return null;
        }

        transform.localPosition = initialPosition;

        gameObject.layer = 6; // Reativa a interação após a animação
    }

    public void ActivateLight()
    {
        lightObject.SetActive(true);
    }

    public void DeactivateLight()
    {
        lightObject.SetActive(false);
    }

    public void Connected()
    {
        isConnected = true;
    }
}
