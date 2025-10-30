using UnityEngine;

[System.Serializable]
public class RecipeIngredientRequirement
{
    public IngredientSO ingredient;
    public int amount;
}

public enum StatType
{
    MaxHealth,
    Damage,
    FireRate,
    MoveSpeed,
}

[CreateAssetMenu(menuName = "Avocado/Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite recipeIcon;

    public RecipeIngredientRequirement[] requirements;

    [Header("Reward")]
    public StatType statToBoost;
    public int boostAmount;
}
