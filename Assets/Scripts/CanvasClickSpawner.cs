using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasClickSpawner : MonoBehaviour
{
    [Header("References")]
    //public RectTransform targetImage;   // Image на Canvas
    public Camera worldCamera;           // Main Camera
    public GameObject spawnPrefab;       // что спавним

    [Header("Cooldown")]
    public float spawnCooldown = 1f;

    private bool canSpawn;

    void OnEnable()
    {
        canSpawn = true;
    }

    void Update()
    {
        if (!canSpawn)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;

          
                Spawn(mousePos);
                StartCoroutine(Cooldown());
            
        }
    }

    // --------------------
    // SPAWN
    // --------------------

    void Spawn(Vector2 screenPos)
    {
        if (!spawnPrefab || !worldCamera)
            return;

        Vector3 worldPos = worldCamera.ScreenToWorldPoint(
            new Vector3(screenPos.x, screenPos.y, transform.position.z)
        );

        worldPos.z = 0f;
        Vector3 pos = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        Instantiate(spawnPrefab, pos, Quaternion.identity,transform);
    }

    // --------------------
    // COOLDOWN
    // --------------------

    IEnumerator Cooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
