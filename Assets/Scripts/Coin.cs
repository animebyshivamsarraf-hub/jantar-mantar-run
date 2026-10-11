using UnityEngine;

/// <summary>Spinning collectible coin. Collects on player trigger.</summary>
[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour
{
    bool collected;

    void Awake()
    {
        BuildVisual();
    }

    void BuildVisual()
    {
        Greybox.Cylinder("CoinMesh", Vector3.zero, new Vector3(0.7f, 0.12f, 0.7f),
            new Color(1f, 0.8f, 0.15f), transform);
        Greybox.Cylinder("Rim", Vector3.zero, new Vector3(0.78f, 0.08f, 0.78f),
            new Color(0.85f, 0.6f, 0.1f), transform);
    }

    void Update()
    {
        transform.Rotate(0f, 180f * Time.deltaTime, 0f);
        var pc = PlayerControllerHolder.Player;
        if (pc != null && transform.position.z < pc.position.z - 12f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (other.GetComponent<PlayerController>() == null) return;
        collected = true;
        if (GameManager.Instance != null) GameManager.Instance.AddCoin();
        AudioManager.Instance?.PlaySfx("coin");
        Destroy(gameObject);
    }
}
