using UnityEngine;

public class PipePuzzle : MonoBehaviour
{
    [SerializeField] private Pipe pipe1;
    [SerializeField] private Pipe pipe2;
    [SerializeField] private Pipe pipe3;
    [Space(10)]

    [SerializeField] private int pipe1correctPosition;
    [SerializeField] private int pipe2correctPosition;
    [SerializeField] private int pipe3correctPosition;

    [Space(10)]
    [SerializeField] private Valve valve1;
    [SerializeField] private Valve valve2;
    [SerializeField] private Valve valve3;

    [Space(10)]
    [SerializeField] private Door door;


    private void Start()
    {
        valve1.SetPipe(pipe1);
        valve2.SetPipe(pipe2);
        valve3.SetPipe(pipe3);
    }

    private void OnEnable()
    {
        pipe1.OnPipeChanged += CheckPuzzle;
        pipe2.OnPipeChanged += CheckPuzzle;
        pipe3.OnPipeChanged += CheckPuzzle;
    }

    private void OnDisable()
    {
        pipe1.OnPipeChanged -= CheckPuzzle;
        pipe2.OnPipeChanged -= CheckPuzzle;
        pipe3.OnPipeChanged -= CheckPuzzle;
    }

    private void CheckPuzzle()
    {
        if (pipe1.GetPosition() == pipe1correctPosition && pipe2.GetPosition() == pipe2correctPosition && pipe3.GetPosition() == pipe3correctPosition)
        {
            valve1.PuzzleCompleted();
            valve2.PuzzleCompleted();
            valve3.PuzzleCompleted();

            door.UnlockDoor();
            GameManager.Instance.Puzzle4Completed();
        }
    }
}
