using UnityEngine;
using UnityEngine.UI;

public class SettingsSwitcher : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource[] sfxSources;

    [Header("UI Images")]
    public Image sfxImage;


    [Header("Sprites")]
    public Sprite onSprite;
    public Sprite offSprite;

    [Header("States")]
    public bool sfxOn = true;
    // третья настройка
  
    [Header("Transparency Settings")]
    [Range(0f, 1f)] public float offAlpha = 0.5f; // прозрачность, когда выключено
    [Range(0f, 1f)] public float onAlpha = 1f;    // прозрачность, когда включено

    private void Start()
    {
        
        ApplySfxState();
      
    }

    // --------------------
    // SWITCHERS
    // --------------------


    public void SwitchSfx()
    {
        sfxOn = !sfxOn;
        ApplySfxState();
    }



    // --------------------
    // APPLY STATES
    // --------------------

 
    void ApplySfxState()
    {
        foreach (AudioSource sfx in sfxSources)
        {
            if (sfx != null)
                sfx.mute = !sfxOn;
        }

        ApplyImageState(sfxImage, sfxOn);
    }

 
    // HELPER
    // --------------------
    void ApplyImageState(Image img, bool state)
    {
        if (img == null) return;

        // прозрачность
        Color c = img.color;
        c.a = state ? onAlpha : offAlpha;
        img.color = c;

        // спрайт
        if (onSprite != null && offSprite != null)
        {
            img.sprite = state ? onSprite : offSprite;
            img.SetNativeSize();
        }
    }
}
