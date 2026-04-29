using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private Light lampLight;
    [SerializeField] private Material lampMaterial;

    private void Awake()
    {
        lampLight.enabled = false;
        lampMaterial.DisableKeyword("_EMISSION");
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPuzzle1Completed += LightUp;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPuzzle1Completed -= LightUp;
    }

    private void LightUp()
    {
        lampLight.enabled = true;
        lampMaterial.EnableKeyword("_EMISSION");
    }
}