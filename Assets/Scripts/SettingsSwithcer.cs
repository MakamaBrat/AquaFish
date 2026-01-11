using UnityEngine;
using UnityEngine.UI;

public class SettingsSwitcher : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    [Header("UI Images")]
    public Image musicImage;
    public Image sfxImage;
    public Image dummyImage;

    [Header("Sprites")]
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;

    public Sprite dummyOnSprite;
    public Sprite dummyOffSprite;

    [Header("States")]
    public bool musicOn = true;
    public bool sfxOn = true;
    public bool dummyOn = true;

    private void Start()
    {
        ApplyMusicState();
        ApplySfxState();
        ApplyDummyState();
    }

    // --------------------
    // SWITCHERS
    // --------------------

    public void SwitchMusic()
    {
        musicOn = !musicOn;
        ApplyMusicState();
    }

    public void SwitchSfx()
    {
        sfxOn = !sfxOn;
        ApplySfxState();
    }

    public void SwitchDummy()
    {
        dummyOn = !dummyOn;
        ApplyDummyState();
    }

    // --------------------
    // APPLY STATES
    // --------------------

    void ApplyMusicState()
    {
        if (musicSource != null)
            musicSource.mute = !musicOn;

        ApplySprite(musicImage, musicOn, musicOnSprite, musicOffSprite);
    }

    void ApplySfxState()
    {
        foreach (var sfx in sfxSources)
            if (sfx != null)
                sfx.mute = !sfxOn;

        ApplySprite(sfxImage, sfxOn, sfxOnSprite, sfxOffSprite);
    }

    void ApplyDummyState()
    {
        ApplySprite(dummyImage, dummyOn, dummyOnSprite, dummyOffSprite);
    }

    // --------------------
    // HELPER
    // --------------------

    void ApplySprite(Image img, bool state, Sprite onSprite, Sprite offSprite)
    {
        if (img == null)
            return;

        img.sprite = state ? onSprite : offSprite;
        img.SetNativeSize();
    }
}
