using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 5;
    public float lifeTime = 2f;

    private Rigidbody2D rb;
    private Vector2 direction;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si choca contra enemigo, hace daño y se destruye
        EnemyFollow enemy = other.GetComponent<EnemyFollow>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
