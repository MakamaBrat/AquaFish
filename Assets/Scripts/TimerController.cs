using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;
    public CanvasClickSpawner canvasClickSpawner;
    [Header("Timer Settings")]
    public float startTime = 60f; // секунды (60 = 1 минута)

    private float currentTime;
    private bool isRunning;
    public AudioSource au;
    public Transform winWin;
    // --------------------
    // LIFECYCLE
    // --------------------

    private void OnEnable()
    {
        RestartTimer();
        winWin.gameObject.SetActive(false);
        canvasClickSpawner.enabled = true;
    }

    void Update()
    {
        if (!isRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            UpdateView();
            isRunning = false;
            GameOver();
            return;
        }

        UpdateView();
    }

    // --------------------
    // TIMER LOGIC
    // --------------------

    public void RestartTimer()
    {
        currentTime = startTime;
        isRunning = true;
        UpdateView();
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    // --------------------
    // VIEW
    // --------------------

    void UpdateView()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    // --------------------
    // GAME OVER
    // --------------------

    void GameOver()
    {
        // 🔴 ТУТ ТВОЯ ЛОГИКА
        // например:
        // menuTravel.makeMenu(3);
        // Time.timeScale = 0f;
        winWin.gameObject.SetActive(true);
        au.Play();
        canvasClickSpawner.enabled = false;
        Debug.Log("Game Over: Timer ended");
    }
}
