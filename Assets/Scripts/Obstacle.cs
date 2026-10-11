using UnityEngine;

/// <summary>
/// A single obstacle. Pass conditions are checked against the player's
/// current move state; unfair hits are impossible by construction:
/// every spawned row always leaves at least one open lane.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    public enum Kind { Barricade, Car, Overhead }

    public Kind kind;
    public float oncomingSpeed; // >0 for oncoming traffic (moves toward -Z)

    bool hitDone;
    Transform visual;

    void Awake()
    {
        BuildVisual();
    }

    void BuildVisual()
    {
        var col = GetComponent<BoxCollider>();
        visual = new GameObject("Visual").transform;
        visual.SetParent(transform, false);
        switch (kind)
        {
            case Kind.Barricade:
                // metal police barricade: striped barrier on feet (jumpable)
                Greybox.Box("FootL", new Vector3(-0.8f, 0.15f, 0), new Vector3(0.25f, 0.3f, 0.9f),
                    new Color(0.3f, 0.3f, 0.32f), visual);
                Greybox.Box("FootR", new Vector3(0.8f, 0.15f, 0), new Vector3(0.25f, 0.3f, 0.9f),
                    new Color(0.3f, 0.3f, 0.32f), visual);
                for (int i = 0; i < 3; i++)
                    Greybox.Box("Stripe" + i, new Vector3(0, 0.55f + i * 0.28f, 0), new Vector3(2.0f, 0.2f, 0.12f),
                        i % 2 == 0 ? new Color(0.9f, 0.85f, 0.2f) : Color.white, visual);
                col.center = new Vector3(0, 0.6f, 0);
                col.size = new Vector3(2.0f, 1.2f, 0.6f);
                break;
            case Kind.Car:
                // auto-rickshaw-ish / car block: tall, must change lane
                var bodyC = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.7f), Random.Range(0.1f, 0.8f));
                Greybox.Box("Body", new Vector3(0, 0.7f, 0), new Vector3(1.9f, 0.9f, 3.6f), bodyC, visual);
                Greybox.Box("Cabin", new Vector3(0, 1.45f, -0.2f), new Vector3(1.6f, 0.7f, 1.8f),
                    new Color(0.15f, 0.2f, 0.28f), visual);
                for (int w = 0; w < 4; w++)
                    Greybox.Cylinder("Wheel" + w,
                        new Vector3(w % 2 == 0 ? -0.95f : 0.95f, 0.35f, w < 2 ? -1.2f : 1.2f),
                        new Vector3(0.7f, 0.3f, 0.7f), Color.black, visual);
                Greybox.Box("LightL", new Vector3(-0.6f, 0.8f, -1.85f), new Vector3(0.35f, 0.25f, 0.1f),
                    new Color(1f, 0.95f, 0.6f), visual);
                Greybox.Box("LightR", new Vector3(0.6f, 0.8f, -1.85f), new Vector3(0.35f, 0.25f, 0.1f),
                    new Color(1f, 0.95f, 0.6f), visual);
                col.center = new Vector3(0, 0.9f, 0);
                col.size = new Vector3(1.9f, 1.8f, 3.6f);
                break;
            case Kind.Overhead:
                // low hanging banner/checkpost: slide under it
                Greybox.Box("PostL", new Vector3(-1.05f, 1.1f, 0), new Vector3(0.25f, 2.2f, 0.25f),
                    new Color(0.35f, 0.35f, 0.38f), visual);
                Greybox.Box("PostR", new Vector3(1.05f, 1.1f, 0), new Vector3(0.25f, 2.2f, 0.25f),
                    new Color(0.35f, 0.35f, 0.38f), visual);
                Greybox.Box("Banner", new Vector3(0, 1.75f, 0), new Vector3(2.35f, 0.7f, 0.15f),
                    new Color(0.85f, 0.25f, 0.15f), visual);
                col.center = new Vector3(0, 1.6f, 0);
                col.size = new Vector3(2.35f, 1.1f, 0.5f);
                break;
        }
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.State != GameManager.RunState.Playing)
            return;
        if (oncomingSpeed > 0f)
            transform.position += Vector3.back * oncomingSpeed * Time.deltaTime;
        // cleanup far behind
        var pc = PlayerControllerHolder.Player;
        if (pc != null && transform.position.z < pc.position.z - 15f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hitDone) return;
        var player = other.GetComponent<PlayerController>();
        if (player == null) return;
        if (GameManager.Instance == null || GameManager.Instance.State != GameManager.RunState.Playing) return;

        bool passed = false;
        switch (kind)
        {
            case Kind.Barricade: passed = player.IsAirborneHigh; break;
            case Kind.Overhead: passed = player.IsSliding; break;
            case Kind.Car: passed = false; break; // must change lane
        }

        hitDone = true;
        if (passed)
        {
            AudioManager.Instance?.PlaySfx("dodge");
        }
        else
        {
            player.Stumble();
            if (PoliceChase.Instance != null) PoliceChase.Instance.AddPressure(25f);
        }
        Destroy(gameObject);
    }
}

/// <summary>Tiny holder so obstacles can find the player without scene lookups.</summary>
public static class PlayerControllerHolder
{
    public static Transform Player { get; set; }
}
