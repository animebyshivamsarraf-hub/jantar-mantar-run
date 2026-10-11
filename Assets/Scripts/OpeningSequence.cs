using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Opening scene: title, slogan, tap-to-start. Then loads the Game scene.
/// Fictional action-comedy; no real persons or incidents depicted.
/// </summary>
public class OpeningSequence : MonoBehaviour
{
    void Start()
    {
        BuildUI();
        AudioManager.Instance?.PlaySfx("siren");
    }

    void BuildUI()
    {
        var canvas = new GameObject("OpeningCanvas");
        var c = canvas.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        // backdrop gradient-ish: dark blue panel
        var bg = new GameObject("BG");
        bg.transform.SetParent(canvas.transform, false);
        var bgImg = bg.AddComponent<Image>();
        bgImg.color = new Color(0.04f, 0.06f, 0.14f);
        var bgRt = bg.GetComponent<RectTransform>();
        bgRt.anchorMin = Vector2.zero; bgRt.anchorMax = Vector2.one;
        bgRt.offsetMin = bgRt.offsetMax = Vector2.zero;

        AddText(canvas.transform, "🛺💨", 90, new Vector2(0, 220));
        AddText(canvas.transform, "JANTAR MANTAR RUN", 54, new Vector2(0, 120));
        var sub = AddText(canvas.transform, "— Delhi Chase —", 30, new Vector2(0, 60));
        sub.color = new Color(1f, 0.75f, 0.3f);
        var slogan = AddText(canvas.transform, "\"Jai Hind! Inquilab Zindabad!\"", 28, new Vector2(0, -10));
        slogan.color = new Color(0.7f, 0.95f, 0.7f);
        var hint = AddText(canvas.transform, "Police tumhare peeche hai!\nSwipe: lane • Up: jump • Down: slide", 24, new Vector2(0, -90));
        hint.color = new Color(0.75f, 0.8f, 0.9f);

        var btnGo = new GameObject("StartBtn");
        btnGo.transform.SetParent(canvas.transform, false);
        var btnImg = btnGo.AddComponent<Image>();
        btnImg.color = new Color(0.15f, 0.6f, 0.25f);
        var btn = btnGo.AddComponent<Button>();
        var rt = btnGo.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(0, -210);
        rt.sizeDelta = new Vector2(380, 100);
        AddText(btnGo.transform, "▶  DAUD LAGAO!", 34, Vector2.zero);
        btn.onClick.AddListener(() =>
        {
            if (GameManager.Instance != null) GameManager.Instance.LoadGameScene();
            else UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        });
    }

    Text AddText(Transform parent, string msg, int size, Vector2 pos)
    {
        var go = new GameObject("txt");
        go.transform.SetParent(parent, false);
        var t = go.AddComponent<Text>();
        t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        t.text = msg;
        t.fontSize = size;
        t.alignment = TextAnchor.MiddleCenter;
        t.color = Color.white;
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;
        rt.sizeDelta = new Vector2(900, 120);
        return t;
    }
}
