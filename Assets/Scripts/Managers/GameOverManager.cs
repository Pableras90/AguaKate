using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Rules")]
    [SerializeField] private int victorySeconds = 60; // objetivo de supervivencia (ajústalo)

    private bool ended = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        if (panel != null) panel.SetActive(false);
    }

    private void Update()
    {
        // Victoria por tiempo
        if (!ended && TimeManager.Instance != null &&
            TimeManager.Instance.elapsedTime >= victorySeconds)
        {
            ShowEnd(victory: true);
        }
    }

    public void GameOver() => ShowEnd(victory: false);

    private void ShowEnd(bool victory)
    {
        if (ended) return;
        ended = true;

        // Pausar juego
        Time.timeScale = 0f;

        if (panel != null) panel.SetActive(true);

        if (titleText != null) titleText.text = victory ? "VICTORIA" : "DERROTA";
        if (subtitleText != null) subtitleText.text = victory
            ? "Has sobrevivido al desafío."
            : "Te han derrotado. ¡Vuelve a intentarlo!";

        if (timerText != null)
        {
            string t = (TimeManager.Instance != null)
                ? TimeManager.Instance.GetFormattedTime()
                : "--:--";
            timerText.text = $"Tiempo: {t}";
        }
    }

    // Hooks para el futuro (botones)
    public void Retry()
    {
        Time.timeScale = 1f; // reanudar
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f; // reanudar
        // Opción A: por nombre (pon el nombre real de tu escena de menú)
        SceneManager.LoadScene("MainMenu");

    }
}