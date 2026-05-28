using System;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnPuzzle1Completed;
    public event Action OnPuzzle2Completed;
    public event Action OnPuzzle3Completed;
    public event Action OnPuzzle4Completed;
    public event Action OnGameStart;
    public event Action<bool> OnGameEnd;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameScene());
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuScene());
    }

    private IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 0f;
        Player.Instance.EnableControls();
    }

    private IEnumerator LoadMainMenuScene()
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
        yield return null;
    }

    public void Puzzle1Completed()
    {
        OnPuzzle1Completed?.Invoke();
    }

    public void Puzzle2Completed() 
    { 
        OnPuzzle2Completed?.Invoke(); 
    }

    public void Puzzle3Completed()
    {
        OnPuzzle3Completed?.Invoke();
    }

    public void Puzzle4Completed()
    {
        OnPuzzle4Completed?.Invoke();
    }

    public void GameStarted()
    {
        OnGameStart?.Invoke();
    }

    public void GameEnded(bool hasWon)
    {
        OnGameEnd?.Invoke(hasWon);
    }
}
