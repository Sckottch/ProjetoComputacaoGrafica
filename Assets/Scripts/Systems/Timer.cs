using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLimit = 300f;
    [SerializeField] private TextMeshProUGUI timeText;

    private float currentTime;
    private bool isRunning;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += StartTimer;
        GameManager.Instance.OnPuzzle4Completed += StopTimer;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= StartTimer;
        GameManager.Instance.OnPuzzle4Completed -= StopTimer;
    }

    private void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;

            UpdateTimeUI();

            GameManager.Instance.GameEnded(false);
        }

        UpdateTimeUI();
    }

    private void StartTimer()
    {
        currentTime = timeLimit;
        isRunning = true;
        Time.timeScale = 1f;
    }

    private void StopTimer()
    {
        isRunning = false;
    }

    private void UpdateTimeUI()
    {
        int minutes = (int)(currentTime / 60f);
        int seconds = (int)(currentTime % 60f);
        timeText.text = $"{minutes:0}:{seconds:00}";
    }
}
