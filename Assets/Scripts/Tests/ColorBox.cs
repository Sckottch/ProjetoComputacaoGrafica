using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorBox : MonoBehaviour, IInteractable
{
    private MeshRenderer meshRenderer;
    private Color originalColor;
    private bool isChanged = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    public void OnInteract()
    {
        if (isChanged)
        {
            meshRenderer.material.color = originalColor;
            Debug.Log($"ColorBox at {transform.position} reverted to original color {originalColor}");

            isChanged = false;
            return;
        }

        meshRenderer.material.color = Random.ColorHSV();
        isChanged = true;

        Debug.Log($"ColorBox at {transform.position} changed color to {meshRenderer.material.color}");
    }
}
