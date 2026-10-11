using UnityEngine;

/// <summary>Stub audio: plays clips when assigned, silently no-ops otherwise.
/// Add licensed/original clips in the inspector later; no audio files are committed.</summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Optional clips (assign in inspector)")]
    public AudioClip jumpClip;
    public AudioClip slideClip;
    public AudioClip coinClip;
    public AudioClip hitClip;
    public AudioClip dodgeClip;
    public AudioClip sirenClip;
    public AudioClip caughtClip;

    AudioSource sfx;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        sfx = gameObject.AddComponent<AudioSource>();
        sfx.playOnAwake = false;
    }

    public void PlaySfx(string name)
    {
        if (sfx == null) return;
        AudioClip c = null;
        switch (name)
        {
            case "jump": c = jumpClip; break;
            case "slide": c = slideClip; break;
            case "coin": c = coinClip; break;
            case "hit": c = hitClip; break;
            case "dodge": c = dodgeClip; break;
            case "siren": c = sirenClip; break;
            case "caught": c = caughtClip; break;
        }
        if (c != null) sfx.PlayOneShot(c);
    }
}
