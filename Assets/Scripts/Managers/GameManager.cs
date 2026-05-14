using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnPuzzle1Completed;
    public event Action OnPuzzle2Completed;

    public void Puzzle1Completed()
    {
        OnPuzzle1Completed?.Invoke();
    }

    public void Puzzle2Completed() 
    { 
        OnPuzzle2Completed?.Invoke(); 
    }
}
