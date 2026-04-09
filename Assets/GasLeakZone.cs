using UnityEngine;

public class GasLeakZone : MonoBehaviour
{
    [Range(0f, 1f)]
    public float leakChance = 0.4f;
    public bool isLeaking = false;

    [Header("Visual")]
    public ParticleSystem leakParticles; // optional wispy effect

    private void Start()
    {
        isLeaking = Random.value < leakChance;

        if (leakParticles != null)
        {
            if (isLeaking)
                leakParticles.Play();
            else
                leakParticles.Stop();
        }
    }
}