using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Builds all UI in code (no scene UI authoring needed):
/// HUD (score/coins/chase bar), warning flash, detention cards, game-over panel.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    Text scoreText;
    Text coinText;
    Image chaseBarFill;
    Text warningText;
    GameObject detentionPanel;
    Text detentionText;
    GameObject gameOverPanel;
    Text finalScoreText;
    float warningTimer;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        BuildUI();
    }

    // ---------- construction ----------
    Canvas MakeCanvas(string name, int sortOrder)
    {
        var go = new GameObject(name);
        var c = go.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        c.sortingOrder = sortOrder;
        go.AddComponent<CanvasScaler>();
        go.AddComponent<GraphicRaycaster>();
        return c;
    }

    Text MakeText(string name, Transform parent, int size, TextAnchor anchor, Vector2 anchoredPos, Vector2 sizeDelta)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var t = go.AddComponent<Text>();
        t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        t.fontSize = size;
        t.alignment = anchor;
        t.color = Color.white;
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = anchoredPos;
        rt.sizeDelta = sizeDelta;
        // outline for readability
        var ol = go.AddComponent<Outline>();
        ol.effectColor = new Color(0, 0, 0, 0.8f);
        ol.effectDistance = new Vector2(2, -2);
        return t;
    }

    Button MakeButton(string label, Transform parent, Vector2 anchoredPos, Vector2 size, UnityEngine.Events.UnityAction onClick)
    {
        var go = new GameObject(label + "Btn");
        go.transform.SetParent(parent, false);
        var img = go.AddComponent<Image>();
        img.color = new Color(0.15f, 0.5f, 0.95f, 0.95f);
        var btn = go.AddComponent<Button>();
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = anchoredPos;
        rt.sizeDelta = size;
        MakeText(label + "Lbl", go.transform, 30, TextAnchor.MiddleCenter, Vector2.zero, size);
        btn.onClick.AddListener(onClick);
        return btn;
    }

    GameObject MakePanel(string name, Transform parent, Color bg)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var img = go.AddComponent<Image>();
        img.color = bg;
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f); rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(640, 420);
        rt.anchoredPosition = Vector2.zero;
        return go;
    }

    void BuildUI()
    {
        // HUD
        var hud = MakeCanvas("HUD", 1);
        scoreText = MakeText("Score", hud.transform, 34, TextAnchor.UpperLeft, new Vector2(-330, 240), new Vector2(500, 60));
        coinText = MakeText("Coins", hud.transform, 30, TextAnchor.UpperLeft, new Vector2(-330, 190), new Vector2(500, 50));
        coinText.color = new Color(1f, 0.85f, 0.3f);
        var chaseLabel = MakeText("ChaseLbl", hud.transform, 22, TextAnchor.UpperRight, new Vector2(330, 240), new Vector2(300, 40));
        chaseLabel.text = "POLICE";
        var barBg = new GameObject("ChaseBarBg");
        barBg.transform.SetParent(hud.transform, false);
        var bgImg = barBg.AddComponent<Image>();
        bgImg.color = new Color(0, 0, 0, 0.5f);
        var bgRt = barBg.GetComponent<RectTransform>();
        bgRt.anchorMin = bgRt.anchorMax = new Vector2(0.5f, 0.5f);
        bgRt.anchoredPosition = new Vector2(220, 205);
        bgRt.sizeDelta = new Vector2(260, 26);
        var fillGo = new GameObject("ChaseBarFill");
        fillGo.transform.SetParent(barBg.transform, false);
        chaseBarFill = fillGo.AddComponent<Image>();
        chaseBarFill.color = new Color(0.95f, 0.25f, 0.2f);
        var fRt = fillGo.GetComponent<RectTransform>();
        fRt.anchorMin = new Vector2(0, 0.5f); fRt.anchorMax = new Vector2(0, 0.5f);
        fRt.pivot = new Vector2(0, 0.5f);
        fRt.anchoredPosition = new Vector2(-130, 0);
        fRt.sizeDelta = new Vector2(260, 26);

        warningText = MakeText("Warning", hud.transform, 36, TextAnchor.MiddleCenter, new Vector2(0, 120), new Vector2(900, 80));
        warningText.color = new Color(1f, 0.4f, 0.3f);
        warningText.gameObject.SetActive(false);

        // Detention overlay (hidden)
        var det = MakeCanvas("Detention", 5);
        detentionPanel = MakePanel("DetPanel", det.transform, new Color(0.02f, 0.02f, 0.05f, 0.92f));
        detentionText = MakeText("DetText", detentionPanel.transform, 34, TextAnchor.MiddleCenter, Vector2.zero, new Vector2(580, 300));
        detentionPanel.SetActive(false);

        // Game over (hidden)
        var goCanvas = MakeCanvas("GameOver", 10);
        gameOverPanel = MakePanel("GOPanel", goCanvas.transform, new Color(0.05f, 0.05f, 0.1f, 0.95f));
        MakeText("GOTitle", gameOverPanel.transform, 52, TextAnchor.MiddleCenter, new Vector2(0, 120), new Vector2(600, 90)).text = "PAKDE GAYE! 🚔";
        finalScoreText = MakeText("GOScore", gameOverPanel.transform, 32, TextAnchor.MiddleCenter, new Vector2(0, 30), new Vector2(600, 60));
        MakeButton("↻ Restart", gameOverPanel.transform, new Vector2(0, -90), new Vector2(320, 90),
            () => { if (GameManager.Instance != null) GameManager.Instance.Restart(); });
        MakeButton("🏠 Menu", gameOverPanel.transform, new Vector2(0, -200), new Vector2(320, 70),
            () => { if (GameManager.Instance != null) GameManager.Instance.LoadOpeningScene(); });
        gameOverPanel.SetActive(false);
    }

    // ---------- runtime ----------
    void Update()
    {
        if (GameManager.Instance == null) return;
        var gm = GameManager.Instance;
        scoreText.text = "Score: " + gm.Score;
        coinText.text = "🪙 " + gm.Coins;
        if (PoliceChase.Instance != null)
        {
            float f = PoliceChase.Instance.Pressure / 100f;
            var rt = chaseBarFill.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(260f * f, 26f);
        }
        if (warningTimer > 0f)
        {
            warningTimer -= Time.deltaTime;
            if (warningTimer <= 0f) warningText.gameObject.SetActive(false);
        }
    }

    public void FlashWarning(string msg, float seconds = 2.5f)
    {
        warningText.text = msg;
        warningText.gameObject.SetActive(true);
        warningTimer = seconds;
    }

    public void ShowDetentionCard(string msg)
    {
        detentionPanel.SetActive(true);
        detentionText.text = msg;
    }

    public void HideDetention() => detentionPanel.SetActive(false);

    public void ShowGameOver()
    {
        HideDetention();
        finalScoreText.text = "Score: " + GameManager.Instance.Score + "   🪙 " + GameManager.Instance.Coins;
        gameOverPanel.SetActive(true);
    }
}
