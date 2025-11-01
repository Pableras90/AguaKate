using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        // Nos suscribimos al evento del TimeManager
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeUpdated += HandleTimeUpdated;
        }
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeUpdated -= HandleTimeUpdated;
        }
    }

    private void HandleTimeUpdated(float elapsed)
    {
        int totalSec = Mathf.FloorToInt(elapsed);
        int minutes = totalSec / 60;
        int seconds = totalSec % 60;
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
