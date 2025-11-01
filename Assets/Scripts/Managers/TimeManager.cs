using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public float elapsedTime { get; private set; } // en segundos

    public event Action<float> OnTimeUpdated; // se llama cada frame con el tiempo
    public event Action<int> OnSecondReached; // se llama cada vez que llegamos a un segundo entero

    private int lastWholeSecond = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // opcional: DontDestroyOnLoad(gameObject); si quieres persistencia entre escenas
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Aviso continuo (para la UI)
        OnTimeUpdated?.Invoke(elapsedTime);

        // Aviso solo cuando pasa cada segundo entero (para oleadas/eventos)
        int currentSecond = Mathf.FloorToInt(elapsedTime);
        if (currentSecond != lastWholeSecond)
        {
            lastWholeSecond = currentSecond;
            OnSecondReached?.Invoke(currentSecond);
        }
    }

    // Utilidad si quieres formatear tipo "01:32"
    public string GetFormattedTime()
    {
        int totalSec = Mathf.FloorToInt(elapsedTime);
        int minutes = totalSec / 60;
        int seconds = totalSec % 60;
        return $"{minutes:00}:{seconds:00}";
    }
}
