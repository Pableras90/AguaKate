using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class IngredientPickup : MonoBehaviour
{
    
    

    private IngredientSO data;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Init(IngredientSO ingredient)
    {
        data = ingredient;
        if (spriteRenderer != null && data != null)
        {
            spriteRenderer.sprite = data.icon;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Añadir al inventario del jugador
        IngredientInventory inv = other.GetComponent<IngredientInventory>();
        if (inv != null && data != null)
        {
            inv.AddIngredient(data, 1);
        }

        Destroy(gameObject);
    }
}
