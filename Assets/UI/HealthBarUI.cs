using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image fillImage;

    private void Update()
    {
        if (playerHealth == null || fillImage == null) return;

        float fill = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        fillImage.fillAmount = Mathf.Clamp01(fill);
    }
}
