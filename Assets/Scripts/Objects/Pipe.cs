using UnityEngine;
using System;
using System.Collections;
using Unity.Mathematics;

public class Pipe : MonoBehaviour
{
    [SerializeField] private int position = 0;
    public event Action OnPipeChanged;

    private readonly int maxPosition = 4;
    private readonly float animationDuration = 0.5f;

    private void Start()
    {
        transform.localRotation = Quaternion.Euler(0, -90, (position * 90));
    }

    public void ChangePosition()
    {
        position++;

        if (position >= maxPosition)
        {
            position = 0;
        }

        RotatePipe();

        PipeChanged();
    }

    private void RotatePipe()
    {
        Quaternion startingAngle = transform.localRotation;

        Quaternion finalAngle = Quaternion.Euler(0f, -90f, (position * 90f));

        StartCoroutine(AnimatePipe(startingAngle, finalAngle, animationDuration));
    }

    private IEnumerator AnimatePipe(Quaternion startingAngle, Quaternion finalAngle, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localRotation = Quaternion.Slerp(startingAngle, finalAngle, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localRotation = finalAngle;
    }

    private void PipeChanged()
    {
        OnPipeChanged?.Invoke();
    }

    public int GetPosition()
    {
        return position;
    }
}
