using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ScaleTimingMiniGame : MonoBehaviour
{
    public ScoreManager scoreManager;
    public FishManager fishManager;
    public Animator animator;

    [Header("Game Circles")]
    public GameObject[] gameCircles;

    [Header("Target")]
    public Transform target;

    [Header("UI")]
    public GameObject startButton;
    public GameObject fingerView;

    [Header("Scale Settings")]
    public float minScale = 0.7f;
    public float maxScale = 1.2f;
    public float speed = 5f;

    [Header("Accuracy")]
    [Range(0f, 0.1f)]
    public float successTolerance = 0.02f;

    [Header("Events")]
    public UnityEvent onSuccess;
    public UnityEvent onFail;

    public AudioSource au0;
    public AudioSource au1;

    private bool goingUp;
    private bool gameStarted;
    private float scaleValue;

    // --------------------
    // LIFECYCLE
    // --------------------

    private void OnEnable()
    {
        ResetAll();
        animator.Play("FishStart");
        fishManager.RestartRound();
    }

    void Update()
    {
        if (!gameStarted)
            return;

        AnimateScale();
    }

    // --------------------
    // START GAME
    // --------------------

    public void StartGame()
    {
        gameStarted = true;

        if (startButton != null)
            startButton.SetActive(false);

        SetGameCircles(true);
        ResetCycle();
    }

    // --------------------
    // SCALE LOGIC
    // --------------------

    void AnimateScale()
    {
        float delta = speed * Time.deltaTime;

        if (goingUp)
        {
            scaleValue += delta;
            if (scaleValue >= maxScale)
            {
                scaleValue = maxScale;
                goingUp = false;
            }
        }
        else
        {
            scaleValue -= delta;
            if (scaleValue <= minScale)
            {
                scaleValue = minScale;
                goingUp = true;
            }
        }

        target.localScale = Vector3.one * scaleValue;
    }

    // --------------------
    // INPUT
    // --------------------

    public void PressButton()
    {
        if (!gameStarted)
            return;

        gameStarted = false; // стопим игру

        if (fingerView != null)
            fingerView.SetActive(true);

        CheckResult();
    }

    // --------------------
    // RESULT
    // --------------------

    void CheckResult()
    {
        bool success = Mathf.Abs(scaleValue - maxScale) <= successTolerance;

        if (success)
        {
            onSuccess.Invoke();
            animator.Play("WinFish");
            fishManager.OnSuccess();
            scoreManager.AddScore(15);
            if (au0) au0.Play();
        }
        else
        {
            onFail.Invoke();
            animator.Play("FishLose");
            if (au1) au1.Play();
        }

        StartCoroutine(RestartAfterDelay());
    }

    // --------------------
    // RESET
    // --------------------

    void ResetAll()
    {
        gameStarted = false;
        goingUp = true;

        scaleValue = minScale;
        target.localScale = Vector3.one * scaleValue;

        if (fingerView != null)
            fingerView.SetActive(false);

        if (startButton != null)
            startButton.SetActive(true);

        SetGameCircles(false);
    }

    void ResetCycle()
    {
        goingUp = true;
        scaleValue = minScale;
        target.localScale = Vector3.one * scaleValue;

        if (fingerView != null)
            fingerView.SetActive(false);
    }

    // --------------------
    // HELPERS
    // --------------------

    void SetGameCircles(bool state)
    {
        foreach (var circle in gameCircles)
        {
            if (circle != null)
                circle.SetActive(state);
        }
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        ResetAll();
        animator.Play("FishStart");
        fishManager.RestartRound();
    }
}
