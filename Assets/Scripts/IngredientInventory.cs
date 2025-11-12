using UnityEngine;
using System;
using System.Collections.Generic;

public class IngredientInventory : MonoBehaviour
{
    public static IngredientInventory Instance; //  para acceder fácilmente desde otros scripts
    public event Action OnInventoryChanged;     //  para avisar a la UI cuando cambia

    private Dictionary<IngredientSO, int> bag = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddIngredient(IngredientSO ingredient, int amount)
    {
        if (ingredient == null || amount <= 0) return;

        if (bag.ContainsKey(ingredient))
            bag[ingredient] += amount;
        else
            bag[ingredient] = amount;

        Debug.Log($"Recogido {ingredient.ingredientName}. Total ahora: {bag[ingredient]}");
        OnInventoryChanged?.Invoke(); //  notificar cambios
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

        OnInventoryChanged?.Invoke(); //  notificar cambios
    }

    // Para debug: listar inventario actual
    public void DebugPrint()
    {
        foreach (var kvp in bag)
        {
            Debug.Log($"{kvp.Key.ingredientName}: {kvp.Value}");
        }
    }

    //  acceso de solo lectura para HUD
    public IReadOnlyDictionary<IngredientSO, int> GetAll() => bag;
}
