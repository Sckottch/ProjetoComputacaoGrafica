using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotateCube : MonoBehaviour, IInteractable
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float duration = 2f;

    private Rigidbody body;
    private Coroutine spinCoroutine;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void OnInteract()
    {
        // Se já estiver girando, reinicia o tempo
        if (spinCoroutine != null)
        {
            StopCoroutine(spinCoroutine);
        }

        spinCoroutine = StartCoroutine(SpinForSeconds(duration, rotationSpeed));
    }

    private IEnumerator SpinForSeconds(float seconds, float angularSpeed)
    {
        body.angularVelocity = Vector3.up * angularSpeed;

        float elapsed = 0f;
        while (elapsed < seconds)
        {
            yield return new WaitForFixedUpdate();
            elapsed += Time.fixedDeltaTime;
        }

        body.angularVelocity = Vector3.zero;
        spinCoroutine = null;
    }
}
