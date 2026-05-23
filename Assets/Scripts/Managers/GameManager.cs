using System;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnPuzzle1Completed;
    public event Action OnPuzzle2Completed;
    public event Action OnPuzzle3Completed;
    public event Action OnPuzzle4Completed;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadSceneAsync(1));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 0f;
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
}
