using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishManager : MonoBehaviour
{
    [Header("Current Fish View")]
    public Image currentFishImage;

    [Header("Fish Sprites (4)")]
    public Sprite[] fishSprites; // ровно 4 рыбки

    [Header("Storage UI")]
    public TMP_Text[] fishCountTexts; // 4 текста под склад

    private int[] fishCounts; // сколько поймано каждой

    private int currentFishIndex;

    private void OnEnable()
    {
        if (fishCounts == null || fishCounts.Length != fishSprites.Length)
            fishCounts = new int[fishSprites.Length];

        UpdateStorageView();
    }

    // --------------------
    // RESTART / NEW ROUND
    // --------------------

    public void RestartRound()
    {
        ChooseRandomFish();
    }

    void ChooseRandomFish()
    {
        currentFishIndex = Random.Range(0, fishSprites.Length);

        currentFishImage.sprite = fishSprites[currentFishIndex];
        currentFishImage.SetNativeSize();
    }

    // --------------------
    // SUCCESS
    // --------------------

    public void OnSuccess()
    {
        fishCounts[currentFishIndex]++;
        UpdateStorageView();
    }

    // --------------------
    // VIEW
    // --------------------

    void UpdateStorageView()
    {
        for (int i = 0; i < fishCountTexts.Length; i++)
        {
            fishCountTexts[i].text = fishCounts[i].ToString();
        }
    }
}
