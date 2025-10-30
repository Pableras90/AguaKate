using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFollow : MonoBehaviour
{
    public float speed = 2f;
    public int maxHealth = 10;
    public int contactDamage = 10;
    public EnemyDropTableSO dropTable;
    public GameObject ingredientPickupPrefab;
    private Transform player;
    private Rigidbody2D rb;
    private int currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        currentHealth = maxHealth;
    }

    private void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
            player = p.transform;

        if (GameManager.Instance != null)
            GameManager.Instance.RegisterEnemy();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (GameManager.Instance != null)
                GameManager.Instance.UnregisterEnemy();
            DropLoot();

            Destroy(gameObject);
        }
    }
    private void DropLoot()
    {
        if (dropTable == null) return;

        foreach (var entry in dropTable.drops)
        {
            float roll = Random.value; // 0..1
            if (roll <= entry.dropChance)
            {
                int count = Random.Range(entry.minAmount, entry.maxAmount + 1);
                for (int i = 0; i < count; i++)
                {
                    SpawnIngredientPickup(entry.ingredient);
                }
            }
        }
    }

    private void SpawnIngredientPickup(IngredientSO ingredient)
    {
        if (ingredientPickupPrefab == null)
        {
            Debug.LogWarning("?? Enemy has no ingredientPickupPrefab assigned!");
            return;
        }

        Vector3 pos = transform.position + Random.insideUnitSphere * 0.3f;
        pos.z = 0f;

        GameObject pickup = Instantiate(ingredientPickupPrefab, pos, Quaternion.identity);

        IngredientPickup script = pickup.GetComponent<IngredientPickup>();
        if (script != null)
        {
            script.Init(ingredient);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Si está tocando al jugador, hace daño poco a poco
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth hp = collision.gameObject.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(contactDamage);
            }
        }
    }
}