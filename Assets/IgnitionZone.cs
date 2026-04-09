using UnityEngine;

public class IgnitionZone : MonoBehaviour
{
    public BurnerController burner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            burner.OnMatchBrought();
        }
    }
}