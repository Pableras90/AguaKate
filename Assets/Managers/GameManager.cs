using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Debug")]
    [SerializeField] private int enemyCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterEnemy()
    {
        enemyCount++;
    }

    public void UnregisterEnemy()
    {
        enemyCount = Mathf.Max(0, enemyCount - 1);
    }
}
