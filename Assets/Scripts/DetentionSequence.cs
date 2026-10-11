using UnityEngine;
using System.Collections;

/// <summary>
/// Short, non-graphic detention cinematic after the catch:
/// dark cards + a police van driving past, then the Game Over panel.
/// </summary>
public class DetentionSequence : MonoBehaviour
{
    bool playing;
    Transform van;

    void Update()
    {
        if (playing) return;
        if (GameManager.Instance != null && GameManager.Instance.State == GameManager.RunState.Caught)
        {
            playing = true;
            StartCoroutine(Play());
        }
    }

    IEnumerator Play()
    {
        GameManager.Instance.BeginDetention();
        BuildVan();

        string[] cards =
        {
            "🚔 Pakde gaye!\n\nPolice ne gher liya...",
            "🚐 Police van aa rahi hai.\n\n(Chinta mat karo — ye sirf khel hai!)",
            "👮 \"Chalo beta, thane chalte hain.\"\n\nSamajhdaari isi me hai — khel me hi daudo! 😄"
        };

        foreach (var card in cards)
        {
            UIManager.Instance?.ShowDetentionCard(card);
            AudioManager.Instance?.PlaySfx("siren");
            yield return new WaitForSeconds(1.8f);
        }

        // van drives past
        float t = 0f;
        Vector3 from = new Vector3(-18f, 0f, 20f), to = new Vector3(18f, 0f, 20f);
        while (t < 1.6f)
        {
            t += Time.deltaTime;
            if (van != null) van.position = Vector3.Lerp(from, to, t / 1.6f);
            yield return null;
        }

        UIManager.Instance?.ShowGameOver();
        GameManager.Instance.ShowGameOver();
    }

    void BuildVan()
    {
        var root = new GameObject("PoliceVan");
        van = root.transform;
        van.position = new Vector3(-18f, 0f, 20f);
        Greybox.Box("VanBody", new Vector3(0, 1.2f, 0), new Vector3(2.4f, 2.0f, 5.5f), Color.white, van);
        Greybox.Box("VanStripe", new Vector3(0, 1.2f, 0), new Vector3(2.44f, 0.5f, 5.54f),
            new Color(0.1f, 0.25f, 0.7f), van);
        Greybox.Box("VanLight", new Vector3(0, 2.45f, 0), new Vector3(1.0f, 0.3f, 0.5f), Color.red, van);
        for (int w = 0; w < 4; w++)
            Greybox.Cylinder("Wheel" + w,
                new Vector3(w % 2 == 0 ? -1.2f : 1.2f, 0.4f, w < 2 ? -1.8f : 1.8f),
                new Vector3(0.8f, 0.35f, 0.8f), Color.black, van);
    }
}
