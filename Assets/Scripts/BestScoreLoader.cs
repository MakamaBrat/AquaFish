using UnityEngine;
using TMPro;

public class BestScoreLoader : MonoBehaviour
{
    public TMP_Text bestText;

    private const string BEST_SCORE_KEY = "BEST_SCORE";

    void OnEnable()
    {
        int best = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        bestText.text = best.ToString();
    }
}
