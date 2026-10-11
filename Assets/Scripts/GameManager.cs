using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Run state machine + score. Single authority for run lifecycle.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum RunState { Opening, Playing, Caught, Detention, GameOver }

    [Header("Difficulty")]
    public float baseSpeed = 10f;
    public float maxSpeed = 24f;
    public float speedRampPerSecond = 0.18f;

    public RunState State { get; private set; } = RunState.Opening;
    public float Distance { get; private set; }
    public int Coins { get; private set; }
    public int Score => Mathf.FloorToInt(Distance) + Coins * 10;
    public float CurrentSpeed { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        CurrentSpeed = baseSpeed;
    }

    public void StartRun()
    {
        if (State == RunState.Playing) return;
        State = RunState.Playing;
        Distance = 0f;
        Coins = 0;
        CurrentSpeed = baseSpeed;
        if (PoliceChase.Instance != null) PoliceChase.Instance.ResetChase();
    }

    void Update()
    {
        if (State != RunState.Playing) return;
        CurrentSpeed = Mathf.Min(maxSpeed, CurrentSpeed + speedRampPerSecond * Time.deltaTime);
        Distance += CurrentSpeed * Time.deltaTime;
    }

    public void AddCoin()
    {
        if (State == RunState.Playing) Coins++;
    }

    /// <summary>Police caught the player: freeze gameplay, play detention.</summary>
    public void OnCaught()
    {
        if (State != RunState.Playing) return; // guard: single trigger
        State = RunState.Caught;
        AudioManager.Instance?.PlaySfx("caught");
    }

    public void BeginDetention()
    {
        if (State != RunState.Caught) return;
        State = RunState.Detention;
    }

    public void ShowGameOver()
    {
        if (State == RunState.GameOver) return;
        State = RunState.GameOver;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadGameScene() => SceneManager.LoadScene("Game");
    public void LoadOpeningScene() => SceneManager.LoadScene("Opening");
}
