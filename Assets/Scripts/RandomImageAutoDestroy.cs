using UnityEngine;
using UnityEngine.UI;

public class RandomImageAutoDestroy : MonoBehaviour
{
    [Header("Image")]
    public Image targetImage;
    public int[] scores;
    [Header("Sprites")]
    public Sprite[] sprites;

    [Header("Auto Destroy")]
    public float lifeTime = 1.5f;

    private void OnEnable()
    {
        ApplyRandomSprite();
        Invoke(nameof(DestroySelf), lifeTime);
    }

    // --------------------
    // LOGIC
    // --------------------

    void ApplyRandomSprite()
    {
        if (targetImage == null || sprites == null || sprites.Length == 0)
            return;

        int index = Random.Range(0, sprites.Length);
        FindAnyObjectByType<ScoreManager>().AddScore(scores[index]);
        targetImage.sprite = sprites[index];
        targetImage.SetNativeSize();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
