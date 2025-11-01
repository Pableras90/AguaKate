using UnityEngine;
using System.Linq;

public class AutoShooter : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject projectilePrefab;
    public float fireRate = 0.5f; // segundos entre disparos
    public float range = 10f;
    public float spawnOffset = 0.3f; // para que la bala no nazca encima del player

    private float timer;
    private PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            ShootClosestEnemy();
        }
    }

    private void ShootClosestEnemy()
    {
        if (projectilePrefab == null) return;

        // Buscar enemigos activos en escena.
        // En Unity nuevas versiones mejor usar FindObjectsByType, pero mantenemos compatibilidad.
#if UNITY_2023_1_OR_NEWER
        EnemyFollow[] enemies = Object.FindObjectsByType<EnemyFollow>(FindObjectsSortMode.None);
#else
        EnemyFollow[] enemies = Object.FindObjectsOfType<EnemyFollow>();
#endif

        if (enemies.Length == 0) return;

        // Elegir el más cercano
        EnemyFollow closest = enemies
            .OrderBy(e => Vector2.Distance(transform.position, e.transform.position))
            .First();

        float dist = Vector2.Distance(transform.position, closest.transform.position);
        if (dist > range) return; // si está demasiado lejos, no disparamos

        // Dirección hacia el enemigo
        Vector2 dir = (closest.transform.position - transform.position).normalized;

        // Posición inicial de la bala un poco delante del jugador
        Vector3 spawnPos = transform.position + (Vector3)(dir * spawnOffset);
        spawnPos.z = 0f;

        // Instanciar bala
        GameObject projGO = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        // Pasar dirección Y daño actual del jugador
        if (projGO.TryGetComponent<Projectile>(out var proj))
        {
            int dmgToUse = (stats != null) ? stats.damage : 1;
            Debug.Log($"[PLAYER] Disparo bala con daño {dmgToUse}");
            proj.Init(dir, dmgToUse);
        }
    }
}
