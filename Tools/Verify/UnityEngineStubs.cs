// UnityEngine API stubs for offline C# verification (Tools/Verify only).
// These let `dotnet build` type-check game scripts without the Unity Editor.
// They implement NO runtime behavior.
using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Object
    {
        public string name;
        public static void Destroy(Object o) { }
        public static void Destroy(GameObject o) { }
        public static T FindObjectOfType<T>() where T : Object => null;
        public static Object[] FindObjectsOfType(Type t) => new Object[0];
        public static GameObject Instantiate(GameObject prefab) => new GameObject(prefab != null ? prefab.name : "Clone");
        public static GameObject Instantiate(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            var go = Instantiate(prefab);
            go.transform.position = pos;
            return go;
        }
    }

    public class GameObject : Object
    {
        public Transform transform;
        public bool activeSelf = true;
        public GameObject() { transform = new Transform(this); }
        public GameObject(string name) : this() { this.name = name; }
        public static GameObject CreatePrimitive(PrimitiveType t) => new GameObject(t.ToString());
        public static GameObject Find(string name) => null;
        public T AddComponent<T>() where T : Component, new() { var c = new T(); c.gameObject = this; return c; }
        public T GetComponent<T>() where T : Component => null;
        public T GetComponentInChildren<T>() where T : Component => null;
        public void SetActive(bool v) { activeSelf = v; }
    }

    public class Component : Object
    {
        public GameObject gameObject;
        public Transform transform => gameObject?.transform;
        public T GetComponent<T>() where T : Component => gameObject != null ? gameObject.GetComponent<T>() : null;
    }

    public class Behaviour : Component { public bool enabled; }
    public class MonoBehaviour : Behaviour
    {
        public Coroutine StartCoroutine(System.Collections.IEnumerator e) => null;
        public void StopCoroutine(Coroutine c) { }
    }
    public class Coroutine : Object { }
    public class WaitForSeconds
    {
        public WaitForSeconds(float seconds) { }
    }

    public class Transform : Component
    {
        public Vector3 position; public Vector3 localPosition;
        public Quaternion rotation; public Quaternion localRotation;
        public Vector3 localScale = Vector3.one;
        public Transform parent;
        public Transform(GameObject go) { gameObject = go; }
        public void SetParent(Transform p, bool worldPositionStays) { parent = p; }
        public void SetParent(Transform p) { parent = p; }
        public Transform Find(string n) => null;
        public void LookAt(Vector3 v) { }
        public void Rotate(float x, float y, float z) { }
        public T GetComponentInChildren<T>() where T : Component => null;
    }

    public struct Vector2
    {
        public float x, y;
        public Vector2(float x, float y) { this.x = x; this.y = y; }
        public static Vector2 zero => new Vector2(0, 0);
        public static Vector2 one => new Vector2(1, 1);
        public float magnitude => (float)Math.Sqrt(x * x + y * y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
    }

    public struct Vector3
    {
        public float x, y, z;
        public Vector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public static Vector3 zero => new Vector3(0, 0, 0);
        public static Vector3 one => new Vector3(1, 1, 1);
        public static Vector3 forward => new Vector3(0, 0, 1);
        public static Vector3 back => new Vector3(0, 0, -1);
        public static Vector3 up => new Vector3(0, 1, 0);
        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector3 operator *(Vector3 a, float d) => new Vector3(a.x * d, a.y * d, a.z * d);
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t) => a + (b - a) * t;
        public static Vector3 MoveTowards(Vector3 c, Vector3 t, float d) => c;
        public static float Distance(Vector3 a, Vector3 b) => 0f;
    }

    public struct Quaternion
    {
        public float x, y, z, w;
        public static Quaternion identity => new Quaternion();
        public static Quaternion Euler(float x, float y, float z) => new Quaternion();
    }

    public static class Mathf
    {
        public const float PI = (float)Math.PI;
        public static float Max(float a, float b) => Math.Max(a, b);
        public static int Max(int a, int b) => Math.Max(a, b);
        public static float Min(float a, float b) => Math.Min(a, b);
        public static int Min(int a, int b) => Math.Min(a, b);
        public static float Clamp(float v, float a, float b) => v < a ? a : v > b ? b : v;
        public static float Lerp(float a, float b, float t) => a + (b - a) * t;
        public static float Abs(float v) => Math.Abs(v);
        public static int FloorToInt(float v) => (int)Math.Floor(v);
        public static float MoveTowards(float c, float t, float d) => c;
        public static float InverseLerp(float a, float b, float v) => (v - a) / (b - a);
        public static float Exp(float v) => (float)Math.Exp(v);
        public static float Sin(float v) => (float)Math.Sin(v);
    }

    public static class Time
    {
        public static float deltaTime => 0.016f;
        public static float time => 0f;
        public static float timeScale = 1f;
    }

    public enum KeyCode { LeftArrow, RightArrow, UpArrow, DownArrow, Space, A, D, W, S, Escape }

    public static class Input
    {
        public static int touchCount => 0;
        public static Touch GetTouch(int i) => new Touch();
        public static Vector2 mousePosition => Vector2.zero;
        public static bool GetMouseButtonDown(int b) => false;
        public static bool GetMouseButtonUp(int b) => false;
        public static bool GetKeyDown(KeyCode k) => false;
    }

    public enum TouchPhase { Began, Moved, Stationary, Ended, Canceled }
    public struct Touch
    {
        public TouchPhase phase;
        public Vector2 position;
    }

    public static class Debug
    {
        public static void Log(object o) { }
        public static void LogWarning(object o) { }
        public static void LogError(object o) { }
    }

    public static class Random
    {
        static System.Random r = new System.Random();
        public static float value => (float)r.NextDouble();
        public static float Range(float a, float b) => a + (float)r.NextDouble() * (b - a);
        public static int Range(int a, int b) => r.Next(a, b);
    }

    public struct Color
    {
        public float r, g, b, a;
        public Color(float r, float g, float b) { this.r = r; this.g = g; this.b = b; this.a = 1; }
        public Color(float r, float g, float b, float a) { this.r = r; this.g = g; this.b = b; this.a = a; }
        public static Color white => new Color(1, 1, 1);
        public static Color black => new Color(0, 0, 0);
        public static Color red => new Color(1, 0, 0);
        public static Color blue => new Color(0, 0, 1);
        public static Color clear => new Color(0, 0, 0, 0);
    }

    public enum PrimitiveType { Sphere, Capsule, Cylinder, Cube, Plane, Quad }

    public class Shader : Object
    {
        public static Shader Find(string name) => new Shader();
    }

    public class Material : Object
    {
        public Color color;
        public Material(Shader s) { }
    }

    public class Renderer : Component { public Material material; }
    public class Collider : Component { public bool isTrigger; }
    public class BoxCollider : Collider { public Vector3 center; public Vector3 size; }
    public class SphereCollider : Collider { public float radius; public Vector3 center; }

    public class Camera : Behaviour { public float fieldOfView = 60f; }

    public class Font : Object { }
    public static class Resources
    {
        public static T GetBuiltinResource<T>(string path) where T : Object, new() => new T();
    }

    public class AudioClip : Object { }
    public class AudioSource : Behaviour
    {
        public bool playOnAwake;
        public void PlayOneShot(AudioClip c) { }
        public void Play() { }
    }

    public class RequireComponentAttribute : Attribute
    {
        public RequireComponentAttribute(Type t) { }
    }
    public class HeaderAttribute : Attribute
    {
        public HeaderAttribute(string h) { }
    }
    public class SerializeFieldAttribute : Attribute { }

    namespace SceneManagement
    {
        public struct Scene { public string name; }
        public static class SceneManager
        {
            public static void LoadScene(string name) { }
            public static Scene GetActiveScene() => new Scene();
        }
    }

    namespace UI
    {
        public class Canvas : Behaviour
        {
            public RenderMode renderMode;
            public int sortingOrder;
        }
        public enum RenderMode { ScreenSpaceOverlay }
        public class CanvasScaler : Behaviour { }
        public class GraphicRaycaster : Behaviour { }
        public class Graphic : Behaviour { public Color color; }
        public class Text : Graphic
        {
            public Font font; public int fontSize; public TextAnchor alignment; public string text;
        }
        public enum TextAnchor { UpperLeft, MiddleCenter, UpperRight }
        public class Image : Graphic { }
        public class Button : Behaviour
        {
            public ButtonClickedEvent onClick = new ButtonClickedEvent();
        }
        public class ButtonClickedEvent
        {
            public void AddListener(Events.UnityAction a) { }
        }
        public class Outline : Behaviour
        {
            public Color effectColor; public Vector2 effectDistance;
        }
        public class RectTransform : Transform
        {
            public Vector2 anchorMin, anchorMax, anchoredPosition, sizeDelta, pivot, offsetMin, offsetMax;
            public RectTransform() : base(null) { }
        }
    }

    namespace Events
    {
        public delegate void UnityAction();
    }
}
