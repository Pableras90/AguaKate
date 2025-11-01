using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class TimedEvent
    {
        public int triggerSecond;   // en qué segundo exacto se dispara
        public string description;  // solo para debug en consola
        public WaveAction action;   // qué hacemos
    }

    public enum WaveAction
    {
        IncreaseEnemySpeed,
        IncreaseSpawnRate,
        SpawnMiniBoss
    }

    [Header("Eventos programados")]
    public TimedEvent[] events;

    private bool[] alreadyTriggered;

    private EnemySpawner spawner;

    private void Start()
    {
        alreadyTriggered = new bool[events.Length];

        // Suscribirnos al tiempo
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnSecondReached += HandleSecondReached;
        }

        // Buscar el spawner en escena (versión moderna Unity)
        spawner = FindFirstObjectByType<EnemySpawner>();
        // Si tu Unity es más viejo y no tiene esto, vuelve a:
        // spawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnSecondReached -= HandleSecondReached;
        }
    }

    private void HandleSecondReached(int seconds)
    {
        for (int i = 0; i < events.Length; i++)
        {
            if (!alreadyTriggered[i] && seconds >= events[i].triggerSecond)
            {
                alreadyTriggered[i] = true;
                Execute(events[i].action);
                Debug.Log($"[WAVE] Evento: {events[i].description} @ t={seconds}s");
            }
        }
    }

    private void Execute(WaveAction action)
    {
        switch (action)
        {
            case WaveAction.IncreaseEnemySpeed:
                IncreaseEnemySpeed();
                break;

            case WaveAction.IncreaseSpawnRate:
                IncreaseSpawnRate();
                break;

            case WaveAction.SpawnMiniBoss:
                SpawnMiniBoss();
                break;
        }
    }

    private void IncreaseEnemySpeed()
    {
        // versión moderna de Unity para obtener todos los enemigos activos en escena
        EnemyFollow[] enemies = FindObjectsByType<EnemyFollow>(FindObjectsSortMode.None);

        foreach (var e in enemies)
        {
            // IMPORTANTE:
            // Aquí supongo que EnemyFollow tiene algo tipo "public float moveSpeed"
            // Si en tu script se llama "speed" o "followSpeed",
            // cambia esta línea por la variable real.
            e.speed *= 1.1f; // +10%
        }
    }

    private void IncreaseSpawnRate()
    {
        if (spawner != null)
        {
            spawner.spawnInterval *= 0.9f; // spawnea más rápido
        }
    }

    private void SpawnMiniBoss()
    {
        if (spawner != null)
        {
            spawner.SpawnMiniBoss();
        }
        else
        {
            Debug.Log("[WAVE] No hay spawner asignado, no puedo spawnear miniboss todavía.");
        }
    }
}
