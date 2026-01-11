using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SquareGoodDetector : MonoBehaviour
{
    [Header("Detection")]
    public string targetTag = "Good";
    public Vector2 boxSize = new Vector2(2f, 2f);
    public LayerMask detectionLayer; // ВАЖНО

    [Header("Timings")]
    public float detectDelay = 1f;
    public float destroyDelay = 1f;

    [Header("Effects")]
    public GameObject hitEffectPrefab;
    public GameObject hitEffectPrefab2;
    bool yes;
    private void OnEnable()
    {
        StartCoroutine(DetectionRoutine());
        yes = false;
    }

    IEnumerator DetectionRoutine()
    {
        yield return new WaitForSeconds(detectDelay);

        DetectObjects();

        yield return new WaitForSeconds(destroyDelay);
        if(yes==false)
        {
            FindAnyObjectByType<ScoreManager>().AddScore(-50);
            Instantiate(hitEffectPrefab2, transform.position, Quaternion.identity, transform.parent.transform);
        }
        Destroy(gameObject);
    }

    // --------------------
    // DETECTION
    // --------------------

    void DetectObjects()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position,
            boxSize,
            0f,
            detectionLayer
        );

        Debug.Log($"Found colliders: {hits.Length}");

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(targetTag))
            {
                Debug.Log("GOOD FOUND: " + hit.name);
                yes = true;
                SpawnEffect(hit.transform.position);
            }
        }
    }

    // --------------------
    // EFFECT
    // --------------------

    void SpawnEffect(Vector3 position)
    {
        if (hitEffectPrefab)
            Instantiate(hitEffectPrefab, position, Quaternion.identity,transform.parent.transform);
    }

    // --------------------
    // DEBUG
    // --------------------

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
