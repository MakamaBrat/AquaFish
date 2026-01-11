using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text textoScore;
    public TMP_Text textoScore2;


    private int scoresBall;
    
    private const string BEST_SCORE_KEY = "BEST_SCORE";
    public AudioSource au;
    public AudioSource au2;
    void OnEnable()
    {
     //   scoresBall = 0;
        UpdateText();
    }
    private void OnDisable()
    {
        StopAndSaveBest();
    }

    void UpdateText()
    {
        textoScore.text =scoresBall.ToString();
        textoScore2.text =scoresBall.ToString();
        
    }

    // ➕ Добавить очки
    public void AddScore(int amount)
    {
        scoresBall += amount;
        if (amount > 0)
        {
            au.Play();
        }
        else { au2.Play(); }
        if(scoresBall<0)
        {
            scoresBall = 0;
        }
        UpdateText();
    }

    // 🔴 ВЫЗЫВАТЬ ПРИ КОНЦЕ ИГРЫ
    public void StopAndSaveBest()
    {
        int best = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);

        if (scoresBall > best)
        {
            PlayerPrefs.SetInt(BEST_SCORE_KEY, scoresBall);
            PlayerPrefs.Save();
        }
    }

    // (опционально)
    public int GetScore()
    {
        return scoresBall;
    }
}
