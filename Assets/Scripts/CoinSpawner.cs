using UnityEngine;

/// <summary>Spawns coin arcs in a random lane ahead of the player.</summary>
public class CoinSpawner : MonoBehaviour
{
    public float spawnAheadDistance = 55f;
    public float interval = 2.2f;
    public int coinsPerArc = 5;

    [Header("Prefabs (assign in inspector)")]
    public GameObject coinPrefab;

    float timer;
    Transform playerTf;

    void Start()
    {
        var pc = FindObjectOfType<PlayerController>();
        playerTf = pc != null ? pc.transform : null;
    }

    void Update()
    {
        if (playerTf == null) return;
        if (GameManager.Instance == null || GameManager.Instance.State != GameManager.RunState.Playing) return;
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = interval * Random.Range(0.8f, 1.3f);
            int lane = Random.Range(0, 3);
            float z = playerTf.position.z + spawnAheadDistance;
            bool arc = Random.value < 0.5f;
            for (int i = 0; i < coinsPerArc; i++)
            {
                if (coinPrefab == null) { Debug.LogWarning("CoinSpawner: coinPrefab missing"); return; }
                float cz = z + i * 2.2f;
                var coin = Instantiate(coinPrefab,
                    new Vector3(PlayerController.LaneX[lane], 1.0f, cz), Quaternion.identity);
                if (arc) coin.transform.position += Vector3.up * Mathf.Sin(i / (float)(coinsPerArc - 1) * Mathf.PI) * 1.6f;
            }
        }
    }
}
