using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Greybox construction helper: builds placeholder art from primitives at runtime.
/// Keeps scenes/prefabs tiny and robust; real art can replace these later.
/// </summary>
public static class Greybox
{
    static readonly Dictionary<Color, Material> matCache = new Dictionary<Color, Material>();

    public static Material Mat(Color c)
    {
        if (!matCache.TryGetValue(c, out var m))
        {
            m = new Material(Shader.Find("Standard"));
            m.color = c;
            matCache[c] = m;
        }
        return m;
    }

    static GameObject Prim(string name, PrimitiveType t, Vector3 pos, Vector3 scale, Color c, Transform parent)
    {
        var go = GameObject.CreatePrimitive(t);
        go.name = name;
        if (parent != null) go.transform.SetParent(parent, false);
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        var r = go.GetComponent<Renderer>();
        if (r != null) r.material = Mat(c);
        return go;
    }

    public static GameObject Box(string name, Vector3 pos, Vector3 scale, Color c, Transform parent = null)
        => Prim(name, PrimitiveType.Cube, pos, scale, c, parent);

    public static GameObject Sphere(string name, Vector3 pos, float diameter, Color c, Transform parent = null)
        => Prim(name, PrimitiveType.Sphere, pos, new Vector3(diameter, diameter, diameter), c, parent);

    public static GameObject Capsule(string name, Vector3 pos, Vector3 scale, Color c, Transform parent = null)
        => Prim(name, PrimitiveType.Capsule, pos, scale, c, parent);

    public static GameObject Cylinder(string name, Vector3 pos, Vector3 scale, Color c, Transform parent = null)
        => Prim(name, PrimitiveType.Cylinder, pos, scale, c, parent);

    /// <summary>Delhi-styled building block: sandstone/white box with window stripes.</summary>
    public static GameObject Building(Vector3 pos, float w, float h, float d, Color baseColor, Transform parent)
    {
        var root = new GameObject("Building");
        root.transform.SetParent(parent, false);
        root.transform.localPosition = pos;
        Box("Body", new Vector3(0, h * 0.5f, 0), new Vector3(w, h, d), baseColor, root.transform);
        // window stripes
        int floors = Mathf.Max(1, Mathf.FloorToInt(h / 3f));
        for (int i = 0; i < floors; i++)
        {
            float y = 2f + i * 3f;
            if (y > h - 1f) break;
            Box("Windows", new Vector3(0, y, d * 0.51f), new Vector3(w * 0.85f, 1.1f, 0.06f),
                new Color(0.12f, 0.16f, 0.22f), root.transform);
        }
        // roof trim (Mughal-ish band)
        Box("Trim", new Vector3(0, h + 0.25f, 0), new Vector3(w * 1.04f, 0.5f, d * 1.04f),
            new Color(0.75f, 0.35f, 0.2f), root.transform);
        return root;
    }

    /// <summary>Simple streetlight: pole + arm + lamp.</summary>
    public static GameObject Streetlight(Vector3 pos, Transform parent)
    {
        var root = new GameObject("Streetlight");
        root.transform.SetParent(parent, false);
        root.transform.localPosition = pos;
        Cylinder("Pole", new Vector3(0, 3f, 0), new Vector3(0.25f, 6f, 0.25f), new Color(0.25f, 0.25f, 0.28f), root.transform);
        Box("Arm", new Vector3(1f, 5.9f, 0), new Vector3(2.2f, 0.18f, 0.18f), new Color(0.25f, 0.25f, 0.28f), root.transform);
        Box("Lamp", new Vector3(2f, 5.75f, 0), new Vector3(0.7f, 0.25f, 0.4f), new Color(1f, 0.95f, 0.7f), root.transform);
        return root;
    }

    /// <summary>Simple tree: trunk + foliage blobs.</summary>
    public static GameObject Tree(Vector3 pos, Transform parent)
    {
        var root = new GameObject("Tree");
        root.transform.SetParent(parent, false);
        root.transform.localPosition = pos;
        float s = Random.Range(0.8f, 1.3f);
        Cylinder("Trunk", new Vector3(0, 1.2f * s, 0), new Vector3(0.4f * s, 2.4f * s, 0.4f * s),
            new Color(0.35f, 0.22f, 0.12f), root.transform);
        Sphere("Leaves", new Vector3(0, 3f * s, 0), 2.6f * s, new Color(0.16f, 0.45f, 0.18f), root.transform);
        return root;
    }
}
