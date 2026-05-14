using UnityEngine;

public class Flask : PickableObject
{
    [Header("Referencias")]
    [SerializeField] private MeshRenderer liquidRenderer;

    [Space(10)]
    [Header("Configuraçãoes")]
    [SerializeField] private Material liquidMaterial;
    [SerializeField] private bool isRadioactive;

    private void Start()
    {
        liquidRenderer.material = liquidMaterial;   
    }

    public void SetBox(Transform anchorPosition)
    {
        Player.Instance.ReleasePickup();

        transform.SetParent(anchorPosition);

        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        body.isKinematic = true;
    }

    public bool IsRadioactive()
    {
        return isRadioactive;
    }
}
