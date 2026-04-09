using UnityEngine;
using TMPro;

public class GasAnalyzer : MonoBehaviour
{
    [Header("References")]
    public TextMeshPro displayText;
    public Renderer screenRenderer;
    public AudioSource alarmAudio;

    [Header("Settings")]
    public float detectionRadius = 0.15f;
    public LayerMask leakLayerMask;

    [Header("Colors")]
    public Color normalColor = Color.green;
    public Color alarmColor = Color.red;

    private bool _leakDetected = false;

    private void Update()
    {
        CheckForLeaks();
    }

    private void CheckForLeaks()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            detectionRadius,
            leakLayerMask
        );

        Debug.Log($"Analyzer hits: {hits.Length} | Position: {transform.position}");

        bool foundLeak = false;

        foreach (Collider hit in hits)
        {
            Debug.Log($"Hit object: {hit.gameObject.name} | Layer: {hit.gameObject.layer}");
            GasLeakZone zone = hit.GetComponent<GasLeakZone>();
            if (zone != null && zone.isLeaking)
            {
                foundLeak = true;
                break;
            }
        }

        if (foundLeak != _leakDetected)
        {
            _leakDetected = foundLeak;
            UpdateDisplay();

            if (foundLeak)
                CompletionTracker.Instance.OnLeakDetected();
        }
    }

    private void UpdateDisplay()
    {
    if (_leakDetected)
    {
        if (displayText != null)
        {
            displayText.text = "УТЕЧКА!";
            displayText.color = alarmColor;
        }
        if (screenRenderer != null)
            screenRenderer.material.color = alarmColor;
        if (alarmAudio != null) alarmAudio.Play();
    }
    else
    {
        if (displayText != null)
        {
            displayText.text = "Норма";
            displayText.color = normalColor;
        }
        if (screenRenderer != null)
            screenRenderer.material.color = normalColor;
        if (alarmAudio != null) alarmAudio.Stop();
    }
    }
}