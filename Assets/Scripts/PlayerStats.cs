using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public int maxHealth = 100;
    public int damage = 5;
    public float fireRate = 0.5f;
    public float moveSpeed = 4f;
    [Header("Defense")]
    public float invulnerabilityTime = 1f; // tiempo base entre golpes


    // Referencias a otros componentes que deben reaccionar
    public PlayerHealth healthComponent;
    public AutoShooter shooterComponent;
    public PlayerController movementComponent;

    private void Start()
    {
        // Sincronizar stats iniciales con los otros scripts
        if (healthComponent != null)
            healthComponent.maxHealth = maxHealth;

        if (shooterComponent != null)
            shooterComponent.fireRate = fireRate;

        if (movementComponent != null)
            movementComponent.moveSpeed = moveSpeed;
    }

    public void ApplyBoost(StatType stat, int amount)
    {
        switch (stat)
        {
            case StatType.MaxHealth:
                maxHealth += amount;
                if (healthComponent != null)
                {
                    healthComponent.maxHealth = maxHealth;
                    healthComponent.currentHealth += amount; // curita bonus opcional
                }
                break;

            case StatType.Damage:
                damage += amount;
                // luego haremos que el damage se pase al Projectile
                break;

            case StatType.FireRate:
                fireRate = Mathf.Max(0.05f, fireRate - (amount * 0.01f));
                // ejemplo: reducir tiempo entre disparos
                if (shooterComponent != null)
                    shooterComponent.fireRate = fireRate;
                break;

            case StatType.MoveSpeed:
                moveSpeed += amount;
                if (movementComponent != null)
                    movementComponent.moveSpeed = moveSpeed;
                break;
            case StatType.InvulnerabilityTime:
                invulnerabilityTime += amount * 0.1f; // ejemplo: cada punto = +0.1s
                if (healthComponent != null)
                {
                    healthComponent.invulnerabilityTime = invulnerabilityTime;
                }
                break;
        }
    }
}
