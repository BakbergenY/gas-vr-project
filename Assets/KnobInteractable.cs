using UnityEngine;
using Oculus.Interaction;

public class KnobInteractable : MonoBehaviour
{
    [Header("References")]
    public BurnerController burner;
    public Transform knobTransform;

    [Header("Settings")]
    public float rotationAmount = 90f;
    public float rotationDuration = 0.3f;

    private bool _isOn = false;
    private bool _isRotating = false;

    // Call this from PointableUnityEventWrapper → OnSelect
    public void Toggle()
    {
        if (_isRotating) return;

        _isOn = !_isOn;
        float targetAngle = _isOn ? rotationAmount : 0f;
        StartCoroutine(RotateKnob(targetAngle));
        burner.OnGasStateChanged(_isOn);
    }

    private System.Collections.IEnumerator RotateKnob(float targetY)
    {
        _isRotating = true;
        Quaternion startRot = knobTransform.localRotation;
        Quaternion endRot = Quaternion.Euler(0f, 0f, targetY);
        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / rotationDuration);
            knobTransform.localRotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        knobTransform.localRotation = endRot;
        _isRotating = false;
    }
}