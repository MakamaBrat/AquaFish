using UnityEngine;
using UnityEngine.UI;

public class ActivateSuccess : MonoBehaviour
{

    public Image sp;
    public Sprite[] sprites;
    public int[] reward;
    public AudioSource au;
   
    private void OnEnable()
    {
        int k = Random.Range(0,3);
    
        sp.sprite = sprites[k];
        sp.SetNativeSize();
        au.Play();
    }
}
