using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

    private Vector2 direction;
    private int damage;
    private Rigidbody2D rb;

    // Llamado justo después de Instantiate
    public void Init(Vector2 dir, int dmg)
    {
        direction = dir.normalized;
        damage = dmg;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
   
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Solo dañamos si tocamos un enemigo
        if (other.TryGetComponent<EnemyFollow>(out var enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
