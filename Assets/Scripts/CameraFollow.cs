using UnityEngine;

/// <summary>Smooth third-person follow camera with a subtle speed FOV kick.</summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 4.6f, -7.6f);
    public Vector3 lookAhead = new Vector3(0f, 1.4f, 5f);
    public float smooth = 6f;

    Camera cam;
    float baseFov = 60f;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam != null) baseFov = cam.fieldOfView;
        if (target == null)
        {
            var pc = FindObjectOfType<PlayerController>();
            if (pc != null) target = pc.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 want = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, want, 1f - Mathf.Exp(-smooth * Time.deltaTime));
        transform.LookAt(target.position + lookAhead);
        if (cam != null && GameManager.Instance != null)
        {
            float t = Mathf.InverseLerp(10f, 24f, GameManager.Instance.CurrentSpeed);
            cam.fieldOfView = Mathf.Lerp(baseFov, baseFov + 8f, t);
        }
    }
}
