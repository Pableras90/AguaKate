using UnityEngine;

public class RecipeCrafter : MonoBehaviour
{
    public PlayerStats playerStats;
    public IngredientInventory inventory;

    public bool CanCraft(RecipeSO recipe)
    {
        foreach (var req in recipe.requirements)
        {
            if (!inventory.HasEnough(req.ingredient, req.amount))
                return false;
        }
        return true;
    }

    public void Craft(RecipeSO recipe)
    {
        if (!CanCraft(recipe))
        {
            Debug.Log("No tienes ingredientes suficientes para " + recipe.recipeName);
            return;
        }

        // Consume ingredientes
        foreach (var req in recipe.requirements)
        {
            inventory.Consume(req.ingredient, req.amount);
        }

        // Aplica mejora
        playerStats.ApplyBoost(recipe.statToBoost, recipe.boostAmount);

        Debug.Log($"Has creado {recipe.recipeName}! {recipe.statToBoost} +{recipe.boostAmount}");
    }
}
