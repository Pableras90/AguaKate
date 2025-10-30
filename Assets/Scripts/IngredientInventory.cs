using UnityEngine;
using System.Collections.Generic;

public class IngredientInventory : MonoBehaviour
{
    private Dictionary<IngredientSO, int> bag = new Dictionary<IngredientSO, int>();

    public void AddIngredient(IngredientSO ingredient, int amount)
    {
        if (bag.ContainsKey(ingredient))
            bag[ingredient] += amount;
        else
            bag[ingredient] = amount;

        Debug.Log($"Recogido {ingredient.ingredientName}. Total ahora: {bag[ingredient]}");
    }

    public bool HasEnough(IngredientSO ingredient, int needed)
    {
        return bag.ContainsKey(ingredient) && bag[ingredient] >= needed;
    }

    public void Consume(IngredientSO ingredient, int amount)
    {
        if (!bag.ContainsKey(ingredient)) return;

        bag[ingredient] -= amount;
        if (bag[ingredient] <= 0)
            bag.Remove(ingredient);
    }

    // Para debug: listar inventario actual
    public void DebugPrint()
    {
        foreach (var kvp in bag)
        {
            Debug.Log($"{kvp.Key.ingredientName}: {kvp.Value}");
        }
    }
}
