using UnityEngine;
using System;

/// <summary>
/// Unified input: touch swipes on device, mouse drag + keyboard in the Editor.
/// Other scripts subscribe to the static events; no scene wiring needed.
/// </summary>
public class TouchInput : MonoBehaviour
{
    public static event Action OnSwipeLeft;
    public static event Action OnSwipeRight;
    public static event Action OnSwipeUp;
    public static event Action OnSwipeDown;
    public static event Action OnTap;

    [Header("Tuning")]
    public float swipeThresholdPx = 60f;
    public float maxTapTime = 0.25f;

    Vector2 pressPos;
    float pressTime;
    bool pressing;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.State != GameManager.RunState.Playing)
            return;

        // --- touch ---
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began) Begin(t.position);
            else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) End(t.position);
            return;
        }

        // --- mouse (editor / desktop test) ---
        if (Input.GetMouseButtonDown(0)) Begin(Input.mousePosition);
        else if (Input.GetMouseButtonUp(0)) End(Input.mousePosition);

        // --- keyboard (editor test) ---
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) OnSwipeLeft?.Invoke();
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) OnSwipeRight?.Invoke();
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) OnSwipeUp?.Invoke();
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) OnSwipeDown?.Invoke();
    }

    void Begin(Vector2 p)
    {
        pressing = true;
        pressPos = p;
        pressTime = Time.time;
    }

    void End(Vector2 p)
    {
        if (!pressing) return;
        pressing = false;
        Vector2 d = p - pressPos;
        if (d.magnitude < swipeThresholdPx)
        {
            if (Time.time - pressTime <= maxTapTime) OnTap?.Invoke();
            return;
        }
        if (Mathf.Abs(d.x) > Mathf.Abs(d.y))
        {
            if (d.x > 0) OnSwipeRight?.Invoke(); else OnSwipeLeft?.Invoke();
        }
        else
        {
            if (d.y > 0) OnSwipeUp?.Invoke(); else OnSwipeDown?.Invoke();
        }
    }
}
