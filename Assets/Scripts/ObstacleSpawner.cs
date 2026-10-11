using UnityEngine;

/// <summary>
/// Spawns fair obstacle rows ahead of the player: never blocks all 3 lanes.
/// Density scales with run speed. Supports "surprise" bursts from PoliceChase.
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [Header("Tuning")]
    public float spawnAheadDistance = 65f;
    public float baseInterval = 1.6f;
    public float minInterval = 0.7f;

    [Header("Prefabs (assign in inspector)")]
    public GameObject barricadePrefab;
    public GameObject carPrefab;
    public GameObject overheadPrefab;

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
            SpawnRow();
            float speedT = Mathf.InverseLerp(10f, 24f, GameManager.Instance.CurrentSpeed);
            timer = Mathf.Lerp(baseInterval, minInterval, speedT) * Random.Range(0.85f, 1.2f);
        }
    }

    void SpawnRow()
    {
        float z = playerTf.position.z + spawnAheadDistance;
        // choose 1-2 lanes to block, always leaving a gap
        int blockCount = Random.value < 0.45f ? 1 : 2;
        int[] lanes = { 0, 1, 2 };
        // shuffle
        for (int i = 0; i < lanes.Length; i++)
        {
            int j = Random.Range(i, lanes.Length);
            int t = lanes[i]; lanes[i] = lanes[j]; lanes[j] = t;
        }
        for (int i = 0; i < blockCount; i++)
        {
            Obstacle.Kind k = (Obstacle.Kind)Random.Range(0, 3);
            float oncoming = 0f;
            if (k == Obstacle.Kind.Car && Random.value < 0.35f)
                oncoming = Random.Range(6f, 10f); // oncoming traffic
            SpawnObstacle(k, lanes[i], z + Random.Range(-2f, 2f), oncoming);
        }
    }

    void SpawnObstacle(Obstacle.Kind k, int lane, float z, float oncoming)
    {
        GameObject prefab = k == Obstacle.Kind.Barricade ? barricadePrefab
            : k == Obstacle.Kind.Car ? carPrefab : overheadPrefab;
        if (prefab == null)
        {
            Debug.LogWarning("ObstacleSpawner: prefab missing for " + k);
            return;
        }
        var go = Instantiate(prefab, new Vector3(PlayerController.LaneX[lane], 0f, z), Quaternion.identity);
        var ob = go.GetComponent<Obstacle>();
        if (ob != null) ob.oncomingSpeed = oncoming;
    }

    /// <summary>Surprise event: dense row with exactly one gap + warning.</summary>
    public void SpawnSurpriseBurst()
    {
        if (playerTf == null) return;
        float z = playerTf.position.z + spawnAheadDistance;
        int gapLane = Random.Range(0, 3);
        for (int lane = 0; lane < 3; lane++)
        {
            if (lane == gapLane) continue;
            SpawnObstacle(Random.value < 0.5f ? Obstacle.Kind.Barricade : Obstacle.Kind.Car, lane, z, 0f);
        }
    }
}
