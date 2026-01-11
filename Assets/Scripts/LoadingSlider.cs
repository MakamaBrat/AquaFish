using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingSlider : MonoBehaviour
{
   
    [SerializeField] private float loadDuration = 3f;
    public MenuTravel travel;
    private void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        float elapsed = 0f;


        while (elapsed < loadDuration)
        {
            elapsed += Time.deltaTime;

            yield return null;
        }

   
        OnLoadComplete();
    }

    private void OnLoadComplete()
    {
        travel.makeMenu(0);
    }
}
