using UnityEngine;

/// <summary>
/// Third-person 3-lane runner controller. Deterministic, transform-based
/// (no physics engine dependency): lane snap, jump arc, slide timer.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    public static readonly float[] LaneX = { -2.2f, 0f, 2.2f };

    [Header("Tuning")]
    public float laneSwitchSpeed = 14f;
    public float jumpVelocity = 8.5f;
    public float gravity = -24f;
    public float slideDuration = 0.75f;
    public float stumbleSlowTime = 0.6f;

    int laneIndex = 1;
    float vy;
    bool grounded = true;
    float slideTimer;
    float stumbleTimer;
    float runPhase;

    Transform visual;
    Vector3 visualBaseScale;

    /// <summary>High enough to clear a barricade.</summary>
    public bool IsAirborneHigh => !grounded && transform.position.y > 1.0f;
    public bool IsSliding => slideTimer > 0f;
    public bool IsStumbling => stumbleTimer > 0f;
    public int Lane => laneIndex;

    void Awake()
    {
        PlayerControllerHolder.Player = transform;
        BuildVisual();
    }

    void BuildVisual()
    {
        // Greybox runner: capsule body + sphere head, Delhi-scarf accent.
        var root = new GameObject("Visual");
        root.transform.SetParent(transform, false);
        visual = root.transform;
        Greybox.Capsule("Body", new Vector3(0, 1.0f, 0), new Vector3(0.9f, 1.4f, 0.9f),
            new Color(0.15f, 0.45f, 0.85f), visual);
        Greybox.Sphere("Head", new Vector3(0, 2.05f, 0), 0.62f,
            new Color(0.95f, 0.75f, 0.6f), visual);
        Greybox.Box("Scarf", new Vector3(0, 1.72f, -0.18f), new Vector3(0.7f, 0.22f, 0.5f),
            new Color(0.95f, 0.55f, 0.1f), visual);
        visualBaseScale = visual.localScale;
    }

    void OnEnable()
    {
        TouchInput.OnSwipeLeft += MoveLeft;
        TouchInput.OnSwipeRight += MoveRight;
        TouchInput.OnSwipeUp += Jump;
        TouchInput.OnSwipeDown += Slide;
    }

    void OnDisable()
    {
        TouchInput.OnSwipeLeft -= MoveLeft;
        TouchInput.OnSwipeRight -= MoveRight;
        TouchInput.OnSwipeUp -= Jump;
        TouchInput.OnSwipeDown -= Slide;
    }

    public void MoveLeft() { if (CanControl()) laneIndex = Mathf.Max(0, laneIndex - 1); }
    public void MoveRight() { if (CanControl()) laneIndex = Mathf.Min(2, laneIndex + 1); }

    public void Jump()
    {
        if (!CanControl() || !grounded) return;
        grounded = false;
        vy = jumpVelocity;
        slideTimer = 0f; // jump cancels slide
        AudioManager.Instance?.PlaySfx("jump");
    }

    public void Slide()
    {
        if (!CanControl()) return;
        if (!grounded) { vy = Mathf.Min(vy, -14f); } // slam down
        slideTimer = slideDuration;
        AudioManager.Instance?.PlaySfx("slide");
    }

    bool CanControl()
    {
        return GameManager.Instance != null && GameManager.Instance.State == GameManager.RunState.Playing;
    }

    /// <summary>Called by obstacles on a failed dodge.</summary>
    public void Stumble()
    {
        if (IsStumbling) return;
        stumbleTimer = stumbleSlowTime;
        AudioManager.Instance?.PlaySfx("hit");
    }

    void Update()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.State != GameManager.RunState.Playing)
        {
            // frozen during caught/detention/gameover
            return;
        }

        float speed = GameManager.Instance.CurrentSpeed * (IsStumbling ? 0.45f : 1f);

        // forward run
        transform.position += Vector3.forward * speed * Time.deltaTime;

        // lane snap
        Vector3 p = transform.position;
        p.x = Mathf.MoveTowards(p.x, LaneX[laneIndex], laneSwitchSpeed * Time.deltaTime);

        // vertical
        if (!grounded)
        {
            vy += gravity * Time.deltaTime;
            p.y += vy * Time.deltaTime;
            if (p.y <= 0f) { p.y = 0f; grounded = true; vy = 0f; }
        }
        transform.position = p;

        // slide timer
        if (slideTimer > 0f) slideTimer -= Time.deltaTime;
        if (stumbleTimer > 0f) stumbleTimer -= Time.deltaTime;

        Animate(speed);
    }

    void Animate(float speed)
    {
        runPhase += Time.deltaTime * speed * 1.6f;
        float bob = grounded ? Mathf.Abs(Mathf.Sin(runPhase)) * 0.08f : 0f;
        visual.localPosition = new Vector3(0f, bob, 0f);

        // lean into lane changes
        float laneVel = Mathf.Clamp((LaneX[laneIndex] - transform.position.x) * 0.4f, -0.5f, 0.5f);
        visual.localRotation = Quaternion.Euler(0f, 0f, -laneVel);

        // slide pose: squash; jump pose: stretch
        Vector3 s = visualBaseScale;
        if (IsSliding) visual.localScale = new Vector3(s.x * 1.25f, s.y * 0.55f, s.z * 1.25f);
        else if (!grounded) visual.localScale = new Vector3(s.x * 0.92f, s.y * 1.1f, s.z * 0.92f);
        else visual.localScale = s;

        // stumble flash
        var rend = visual.GetComponentInChildren<Renderer>();
        if (rend != null)
            rend.material.color = IsStumbling && Mathf.FloorToInt(Time.time * 12f) % 2 == 0
                ? Color.red : new Color(0.15f, 0.45f, 0.85f);
    }
}
