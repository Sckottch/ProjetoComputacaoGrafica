using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnPuzzle1Completed;

    public void Puzzle1Completed()
    {
        OnPuzzle1Completed?.Invoke();
    }
}
