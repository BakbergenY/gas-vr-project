using UnityEngine;
using System.Collections;

public class BurnerController : MonoBehaviour
{
    [Header("State")]
    public bool gasIsOn = false;
    public bool isLit = false;

    [Header("References")]
    public ParticleSystem flameParticles;
    public AudioSource cracklingSFX;

    [Header("Shake Settings")]
    public float shakeIntensity = 0.005f;
    public float shakeDuration = 1.5f;

    private Vector3 _originalPosition;
    private bool _isShaking = false;

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    public void OnGasStateChanged(bool gasOn)
    {
        gasIsOn = gasOn;

        if (!gasOn)
        {
            isLit = false;
            flameParticles.Stop();
            if (cracklingSFX != null) cracklingSFX.Stop();
            StopShake();
        }
    }

    public void OnMatchBrought()
    {
        if (gasIsOn && !isLit)
        {
            isLit = true;
            flameParticles.Play();
            if (cracklingSFX != null) cracklingSFX.Play();
            CompletionTracker.Instance.OnBurnerLit();
        }
        else if (!gasIsOn && !_isShaking)
        {
            StartCoroutine(Shake());
        }
    }

    private void StopShake()
    {
        if (_isShaking)
        {
            StopAllCoroutines();
            transform.localPosition = _originalPosition;
            _isShaking = false;
        }
    }

    private IEnumerator Shake()
    {
        _isShaking = true;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeIntensity, shakeIntensity);
            float z = Random.Range(-shakeIntensity, shakeIntensity);
            transform.localPosition = _originalPosition + new Vector3(x, 0f, z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _originalPosition;
        _isShaking = false;
    }
}