using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Normal enemies")]
    public GameObject enemyPrefab;

    [Header("MiniBoss")]
    public GameObject miniBossPrefab;

    [Header("Spawn Timing")]
    public float spawnInterval = 2f;
    private float timer;

    [Header("Spawn Area")]
    public float spawnRadius = 6f;

    // Referencia al jugador para spawnear cerca de él (para miniboss)
    private Transform player;

    private void Start()
    {
        // Intentamos encontrar al jugador por tag
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
        else
        {
            Debug.LogWarning("[EnemySpawner] No se ha encontrado Player con tag 'Player'. El miniboss no sabrá dónde aparecer.");
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Dirección aleatoria alrededor del spawner
        Vector2 dir = Random.insideUnitCircle.normalized;

        // Posición final
        Vector3 pos = transform.position + new Vector3(dir.x, dir.y, 0f) * spawnRadius;

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    public void SpawnMiniBoss()
    {
        if (miniBossPrefab == null)
        {
            Debug.LogWarning("[EnemySpawner] miniBossPrefab no asignado");
            return;
        }

        if (player == null)
        {
            Debug.LogWarning("[EnemySpawner] No tengo referencia de player, spawneo miniboss alrededor del spawner");
            // fallback: cerca del propio spawner si no hay player
            Vector2 fallbackDir = Random.insideUnitCircle.normalized;
            Vector3 fallbackPos = transform.position + new Vector3(fallbackDir.x, fallbackDir.y, 0f) * (spawnRadius * 0.8f);
            fallbackPos.z = 0f;

            Instantiate(miniBossPrefab, fallbackPos, Quaternion.identity);
            Debug.Log("[SPAWNER] Miniboss spawneado (fallback)");
            return;
        }

        // Spawnear alrededor del jugador si lo tenemos localizado
        Vector2 dir = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = player.position + (Vector3)(dir * (spawnRadius * 0.8f));
        spawnPos.z = 0f;

        Instantiate(miniBossPrefab, spawnPos, Quaternion.identity);
        Debug.Log("[SPAWNER] Miniboss spawneado (player-based)");
    }
}
