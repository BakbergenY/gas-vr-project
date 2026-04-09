using UnityEngine;
using TMPro;

public class CompletionTracker : MonoBehaviour
{
    public static CompletionTracker Instance;

    [Header("References")]
    public GameObject completionPanel;
    public TextMeshProUGUI completionText;

    [Header("Completion Flags")]
    public bool leakDetected = false;
    public bool burnerLit = false;

    private void Awake()
    {
        Instance = this;
        if (completionPanel != null)
            completionPanel.SetActive(false);
    }

    public void OnLeakDetected()
    {
        leakDetected = true;
        Debug.Log("Leak detected - flag set");
        CheckCompletion();
    }

    public void OnBurnerLit()
    {
        burnerLit = true;
        Debug.Log("Burner lit - flag set");
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        if (leakDetected && burnerLit)
        {
            Debug.Log("All tasks complete!");
            if (completionPanel != null)
                completionPanel.SetActive(true);
            if (completionText != null)
                completionText.text = "Диагностика пройдена успешно";
        }
    }
}