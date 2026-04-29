using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Breaker : MonoBehaviour, IInteractable
{ 
    [SerializeField] private FuseBox box;

    private readonly int errorHash = Animator.StringToHash("Error");
    private readonly int activateHash = Animator.StringToHash("Activate");

    private Animator animator;

    public bool IsOn { get; private set; } = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnInteract()
    {
        if (box.HasFuse)
        {
            IsOn = true;
            animator.SetTrigger(activateHash);

            gameObject.layer = 0;
            GameManager.Instance.Puzzle1Completed();

            return;
        }

        animator.SetTrigger(errorHash);
    }
}
