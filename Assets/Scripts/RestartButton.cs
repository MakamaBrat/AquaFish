using System.Collections;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public MenuTravel mmm;
    public void Restart()
    {
        //StopAllCoroutines();
        StartCoroutine(Restart2());
    }

    IEnumerator Restart2()
    {
        mmm.makeMenu(0);
        yield return new WaitForSeconds(0.02f);
        mmm.makeMenu(1);
    }
}
