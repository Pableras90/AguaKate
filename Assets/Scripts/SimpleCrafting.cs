using UnityEngine;

public class SimpleCrafting : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerStats playerStats;         // arrastra el Player
    [SerializeField] private IngredientSO orangeSegment;      // arrastra el SO del gajo de naranja

    [Header("Recipe: 5 gajos -> +5 daño")]
    [SerializeField] private int neededSegments = 5;
    [SerializeField] private int damageBonus = 5;

    [Header("Input")]
    [SerializeField] private KeyCode craftKey = KeyCode.Q;

    private void Update()
    {
        if (Input.GetKeyDown(craftKey))
            TryCraftOrangeJuice();
    }

    private void TryCraftOrangeJuice()
    {
        if (IngredientInventory.Instance == null)
        {
            Debug.LogWarning("[CRAFT] No hay IngredientInventory en escena.");
            return;
        }
        if (playerStats == null)
        {
            Debug.LogWarning("[CRAFT] Falta PlayerStats en SimpleCrafting.");
            return;
        }
        if (orangeSegment == null)
        {
            Debug.LogWarning("[CRAFT] Falta asignar el IngredientSO del gajo de naranja.");
            return;
        }

        // ¿Tenemos suficientes gajos?
        if (!IngredientInventory.Instance.HasEnough(orangeSegment, neededSegments))
        {
            int have = IngredientInventory.Instance.GetAll().TryGetValue(orangeSegment, out var c) ? c : 0;
            Debug.Log($"[CRAFT] Faltan gajos ({have}/{neededSegments}).");
            return;
        }

        // Consumir y aplicar mejora
        IngredientInventory.Instance.Consume(orangeSegment, neededSegments);
        playerStats.ApplyBoost(StatType.Damage, damageBonus);

        Debug.Log($"[CRAFT] Zumo de naranja hecho: -{neededSegments} gajos, +{damageBonus} daño. Nuevo daño: {playerStats.damage}");
    }
}
