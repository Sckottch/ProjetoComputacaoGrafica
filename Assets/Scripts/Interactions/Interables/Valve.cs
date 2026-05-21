using System.Collections;
using UnityEngine;

public class Valve : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform valveTransform;
    
    private Pipe pipe; 

    private readonly float animationDuration = 0.5f;
    
    private readonly float rotationAngle = 90f;

    private bool isCompleted = false;

    public void OnInteract()
    {
        if (pipe == null) return;

        pipe.ChangePosition();
        RotateValve();
    }

    private void RotateValve()
    {
        Quaternion startingAngle = valveTransform.localRotation;

        Quaternion finalAngle = startingAngle;
        finalAngle *= Quaternion.Euler(0f, 0f, rotationAngle);

        StartCoroutine(AnimateValve(startingAngle, finalAngle, animationDuration));
    }

    private IEnumerator AnimateValve(Quaternion startingAngle, Quaternion finalAngle, float duration)
    {
        gameObject.layer = 0;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            valveTransform.localRotation = Quaternion.Lerp(startingAngle, finalAngle, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        valveTransform.localRotation = finalAngle;

        gameObject.layer = 6;

        if (isCompleted)
        {
            gameObject.layer = 0;
        }
    }

    public void SetPipe(Pipe pipe)
    {
        this.pipe = pipe;
    }

    public void PuzzleCompleted()
    {
        gameObject.layer = 0;
        isCompleted = true;
    }
}
