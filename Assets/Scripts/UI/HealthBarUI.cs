using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI hpText;

    [Header("Color thresholds")]
    [Range(0f, 1f)] public float warnThreshold = 0.5f;   // <50% vida -> naranja
    [Range(0f, 1f)] public float dangerThreshold = 0.25f; // <25% vida -> rojo
    public Color safeColor = new Color(0.0f, 0.9f, 0.0f);    // verde brillante
    public Color warnColor = new Color(1.0f, 0.6f, 0.0f);    // naranja intenso (#FF9900)
    public Color dangerColor = new Color(0.9f, 0.0f, 0.0f);  // rojo oscuro (#E60000)


    [Header("Smoothing")]
    public float lerpSpeed = 8f; // suavidad de la barra

    private float displayedFill = 1f; // lo que estamos mostrando visualmente ahora mismo

    private void Start()
    {
        // Inicializamos bien al empezar
        if (playerHealth != null && playerHealth.maxHealth > 0)
        {
            displayedFill = (float)playerHealth.currentHealth / playerHealth.maxHealth;
            displayedFill = Mathf.Clamp01(displayedFill);
            ApplyVisualsInstant();
        }
    }

    private void Update()
    {
        if (playerHealth == null || playerHealth.maxHealth <= 0) return;

        // 1. Calcula el fill "real"
        float targetFill = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        targetFill = Mathf.Clamp01(targetFill);

        // 2. Suaviza la transición con Lerp
        displayedFill = Mathf.Lerp(displayedFill, targetFill, Time.deltaTime * lerpSpeed);

        // 3. Actualiza el fill en la imagen
        if (fillImage != null)
        {
            fillImage.fillAmount = displayedFill;
            fillImage.color = GetColorForFill(displayedFill);
        }

        // 4. Actualiza el texto tipo "75 / 100"
        if (hpText != null)
        {
            hpText.text = playerHealth.currentHealth + " / " + playerHealth.maxHealth;
        }
    }

    private void ApplyVisualsInstant()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = displayedFill;
            fillImage.color = GetColorForFill(displayedFill);
        }

        if (hpText != null)
        {
            hpText.text = playerHealth.currentHealth + " / " + playerHealth.maxHealth;
        }
    }

    private Color GetColorForFill(float fill)
    {
        // Ejemplo:
        // >50% → verde
        // 25-50% → naranja
        // <25% → rojo

        if (fill <= dangerThreshold)
        {
            return dangerColor;
        }
        else if (fill <= warnThreshold)
        {
            return warnColor;
        }
        else
        {
            return safeColor;
        }
    }
}
