using UnityEngine;
using System.Linq;

public class AutoShooter : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject projectilePrefab;
    public float fireRate = 0.5f; // segundos entre disparos
    public float range = 10f;

    private float timer;

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

        // Encuentra todos los enemigos activos
        EnemyFollow[] enemies = Object.FindObjectsByType<EnemyFollow>(FindObjectsSortMode.None);
        if (enemies.Length == 0) return;

        // Escoge el más cercano
        EnemyFollow closest = enemies
            .OrderBy(e => Vector2.Distance(transform.position, e.transform.position))
            .First();

        float dist = Vector2.Distance(transform.position, closest.transform.position);
        if (dist > range) return; // si está demasiado lejos, no dispara

        // Calcula dirección
        Vector2 dir = (closest.transform.position - transform.position).normalized;

        // Instancia proyectil
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Projectile projectile = proj.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Init(dir);
        }
    }
}
