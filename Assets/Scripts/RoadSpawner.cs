using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Recycles road segments ahead of the player (object pooling, no GC churn).
/// Each segment carries Delhi streetscape dressing: buildings, streetlights, trees.
/// </summary>
public class RoadSpawner : MonoBehaviour
{
    [Header("Setup")]
    public float segmentLength = 30f;
    public int segmentCount = 8;
    public float roadWidth = 9f;

    readonly List<Transform> segments = new List<Transform>();
    float nextZ;
    Transform playerTf;

    static readonly Color[] BuildingColors =
    {
        new Color(0.85f, 0.72f, 0.55f), // sandstone
        new Color(0.92f, 0.90f, 0.85f), // white
        new Color(0.78f, 0.60f, 0.42f), // tan
        new Color(0.88f, 0.80f, 0.66f), // cream
    };

    void Start()
    {
        var pc = FindObjectOfType<PlayerController>();
        playerTf = pc != null ? pc.transform : null;
        float startZ = playerTf != null ? playerTf.position.z - 20f : -20f;
        nextZ = startZ;
        for (int i = 0; i < segmentCount; i++) SpawnSegment();
    }

    void SpawnSegment()
    {
        var seg = new GameObject("RoadSegment").transform;
        seg.SetParent(transform, false);
        seg.position = new Vector3(0f, 0f, nextZ + segmentLength * 0.5f);

        // asphalt
        Greybox.Box("Road", new Vector3(0, -0.05f, 0), new Vector3(roadWidth, 0.1f, segmentLength),
            new Color(0.16f, 0.16f, 0.18f), seg);
        // lane divider dashes
        for (float z = -segmentLength / 2 + 2f; z < segmentLength / 2; z += 4f)
        {
            Greybox.Box("DashL", new Vector3(-1.1f, 0.01f, z), new Vector3(0.12f, 0.02f, 1.6f), Color.white, seg);
            Greybox.Box("DashR", new Vector3(1.1f, 0.01f, z), new Vector3(0.12f, 0.02f, 1.6f), Color.white, seg);
        }
        // footpaths
        Greybox.Box("PathL", new Vector3(-roadWidth / 2 - 1.5f, 0.05f, 0), new Vector3(3f, 0.2f, segmentLength),
            new Color(0.45f, 0.43f, 0.4f), seg);
        Greybox.Box("PathR", new Vector3(roadWidth / 2 + 1.5f, 0.05f, 0), new Vector3(3f, 0.2f, segmentLength),
            new Color(0.45f, 0.43f, 0.4f), seg);

        // Delhi dressing: buildings both sides with varied heights
        for (int side = -1; side <= 1; side += 2)
        {
            float xBase = side * (roadWidth / 2 + 5.5f);
            for (float z = -segmentLength / 2 + 4f; z < segmentLength / 2; z += 9f)
            {
                if (Random.value < 0.25f) // gap with trees
                {
                    Greybox.Tree(new Vector3(xBase + Random.Range(-1f, 1f), 0f, z + Random.Range(-2f, 2f)), seg);
                    continue;
                }
                float w = Random.Range(5f, 8f), h = Random.Range(7f, 20f), d = Random.Range(5f, 8f);
                Greybox.Building(new Vector3(xBase + Random.Range(-1.5f, 1.5f), 0f, z),
                    w, h, d, BuildingColors[Random.Range(0, BuildingColors.Length)], seg);
            }
            // streetlights
            for (float z = -segmentLength / 2 + 6f; z < segmentLength / 2; z += 15f)
            {
                var lamp = Greybox.Streetlight(new Vector3(side * (roadWidth / 2 + 0.8f), 0f, z), seg);
                if (side < 0) lamp.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        // occasional Jantar Mantar-esque monument silhouette in the distance
        if (Random.value < 0.3f)
        {
            var mon = new GameObject("Monument");
            mon.transform.SetParent(seg, false);
            mon.transform.localPosition = new Vector3(Random.value < 0.5f ? -14f : 14f, 0f, Random.Range(-10f, 10f));
            Greybox.Box("Base", new Vector3(0, 1f, 0), new Vector3(4f, 2f, 4f), new Color(0.72f, 0.3f, 0.22f), mon.transform);
            Greybox.Box("Tower", new Vector3(0, 5f, 0), new Vector3(1.6f, 8f, 1.6f), new Color(0.78f, 0.34f, 0.24f), mon.transform);
            Greybox.Sphere("Top", new Vector3(0, 9.6f, 0), 2f, new Color(0.85f, 0.4f, 0.28f), mon.transform);
        }

        segments.Add(seg);
        nextZ += segmentLength;
    }

    void Update()
    {
        if (playerTf == null) return;
        float recycleLine = playerTf.position.z - 25f;
        foreach (var seg in segments)
        {
            if (seg.position.z + segmentLength * 0.5f < recycleLine)
            {
                seg.position = new Vector3(0f, 0f, nextZ + segmentLength * 0.5f);
                nextZ += segmentLength;
            }
        }
    }
}
