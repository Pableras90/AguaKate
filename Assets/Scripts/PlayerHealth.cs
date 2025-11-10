using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Damage Cooldown")]
    public float invulnerabilityTime = 1f;
    private bool isInvulnerable = false;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return; // evita daño repetido

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityRoutine());
        }
    }
    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        float time = invulnerabilityTime;

        // Efecto visual parpadeando (opcional)
        if (spriteRenderer != null)
        {
            float blinkTime = 0.1f;
            for (int i = 0; i < time / (blinkTime * 2); i++)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(blinkTime);
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(blinkTime);
            }
        }
        else
        {
            yield return new WaitForSeconds(invulnerabilityTime);
        }

        isInvulnerable = false;
    }


    private void Die()
    {
        GameOverManager.Instance?.GameOver();
        Debug.Log("Player died!");
        // Más adelante: trigger Game Over, UI, etc.
        // Por ahora simplemente desactivamos el jugador
        gameObject.SetActive(false);
    }
}
