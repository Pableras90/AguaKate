using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    [Header("Spawn Timing")]
    public float spawnInterval = 2f;


    private float timer;
    [Header("Spawn Area")]
    public float spawnRadius = 6f;

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
        Vector2 dir = Random.insideUnitCircle.normalized;

        Vector3 pos = transform.position + new Vector3(dir.x, dir.y, 0f) * spawnRadius;

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
