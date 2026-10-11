using UnityEngine;

/// <summary>
/// Police pressure system. Stumbles raise pressure; clean running lowers it.
/// At 100 the player is caught. Includes surprise events and a visible
/// police vehicle that closes in as pressure rises.
/// </summary>
public class PoliceChase : MonoBehaviour
{
    public static PoliceChase Instance { get; private set; }

    [Header("Tuning")]
    public float maxPressure = 100f;
    public float decayPerSecond = 3f;
    public float surpriseEveryMin = 22f;
    public float surpriseEveryMax = 38f;

    public float Pressure { get; private set; }

    Transform policeCar;
    Transform lightBarRed;
    Transform lightBarBlue;
    float flashT;
    float surpriseTimer;
    ObstacleSpawner obstacleSpawner;
    Transform playerTf;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        var pc = FindObjectOfType<PlayerController>();
        playerTf = pc != null ? pc.transform : null;
        if (playerTf != null) PlayerControllerHolder.Player = playerTf;
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        BuildPoliceCar();
        surpriseTimer = Random.Range(surpriseEveryMin, surpriseEveryMax);
    }

    void BuildPoliceCar()
    {
        var root = new GameObject("PoliceCar");
        policeCar = root.transform;
        // white police jeep with light bar
        Greybox.Box("Body", new Vector3(0, 0.75f, 0), new Vector3(2.0f, 0.9f, 4.2f), Color.white, policeCar);
        Greybox.Box("Cabin", new Vector3(0, 1.5f, 0.2f), new Vector3(1.7f, 0.7f, 2.0f),
            new Color(0.12f, 0.18f, 0.3f), policeCar);
        Greybox.Box("Stripe", new Vector3(0, 0.75f, 0), new Vector3(2.02f, 0.3f, 4.22f),
            new Color(0.1f, 0.25f, 0.7f), policeCar);
        lightBarRed = Greybox.Box("LightR", new Vector3(-0.4f, 2.0f, 0.2f), new Vector3(0.5f, 0.25f, 0.4f),
            Color.red, policeCar).transform;
        lightBarBlue = Greybox.Box("LightB", new Vector3(0.4f, 2.0f, 0.2f), new Vector3(0.5f, 0.25f, 0.4f),
            Color.blue, policeCar).transform;
        for (int w = 0; w < 4; w++)
            Greybox.Cylinder("Wheel" + w,
                new Vector3(w % 2 == 0 ? -1.0f : 1.0f, 0.35f, w < 2 ? -1.4f : 1.4f),
                new Vector3(0.7f, 0.3f, 0.7f), Color.black, policeCar);
        // two officer figures (capsules)
        Greybox.Capsule("Cop1", new Vector3(-0.5f, 1.6f, -1.2f), new Vector3(0.6f, 1.0f, 0.6f),
            new Color(0.1f, 0.15f, 0.45f), policeCar);
        Greybox.Capsule("Cop2", new Vector3(0.5f, 1.6f, -1.2f), new Vector3(0.6f, 1.0f, 0.6f),
            new Color(0.1f, 0.15f, 0.45f), policeCar);
    }

    public void AddPressure(float amount)
    {
        if (GameManager.Instance == null || GameManager.Instance.State != GameManager.RunState.Playing) return;
        Pressure = Mathf.Min(maxPressure, Pressure + amount);
        if (Pressure >= maxPressure)
            GameManager.Instance.OnCaught(); // catch immediately; Update's decay would otherwise drop it below 100 first
    }

    void Update()
    {
        if (GameManager.Instance == null || playerTf == null) return;
        if (GameManager.Instance.State != GameManager.RunState.Playing)
        {
            policeCar.gameObject.SetActive(GameManager.Instance.State == GameManager.RunState.Playing ||
                                           GameManager.Instance.State == GameManager.RunState.Caught);
            return;
        }

        // decay while running clean
        Pressure = Mathf.Max(0f, Pressure - decayPerSecond * Time.deltaTime);

        // car closes in with pressure (14m behind at 0, 4m behind at 100)
        float gap = Mathf.Lerp(14f, 4f, Pressure / maxPressure);
        Vector3 pp = playerTf.position;
        policeCar.position = new Vector3(pp.x * 0.6f, 0f, pp.z - gap);

        // siren flash
        flashT += Time.deltaTime * 10f;
        bool redOn = Mathf.FloorToInt(flashT) % 2 == 0;
        SetLight(lightBarRed, redOn ? Color.red : new Color(0.4f, 0.05f, 0.05f));
        SetLight(lightBarBlue, redOn ? new Color(0.05f, 0.05f, 0.4f) : Color.blue);

        // surprise events
        surpriseTimer -= Time.deltaTime;
        if (surpriseTimer <= 0f)
        {
            surpriseTimer = Random.Range(surpriseEveryMin, surpriseEveryMax);
            TriggerSurprise();
        }

        if (Pressure >= maxPressure)
            GameManager.Instance.OnCaught();
    }

    void SetLight(Transform t, Color c)
    {
        var r = t.GetComponent<Renderer>();
        if (r != null) r.material.color = c;
    }

    void TriggerSurprise()
    {
        AddPressure(10f);
        if (obstacleSpawner != null) obstacleSpawner.SpawnSurpriseBurst();
        if (UIManager.Instance != null) UIManager.Instance.FlashWarning("⚠️ Police aage se aa rahi hai!");
        AudioManager.Instance?.PlaySfx("siren");
    }

    public void ResetChase()
    {
        Pressure = 0f;
        surpriseTimer = Random.Range(surpriseEveryMin, surpriseEveryMax);
    }
}
