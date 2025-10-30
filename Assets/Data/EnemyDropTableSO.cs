using UnityEngine;

[System.Serializable]
public class DropEntry
{
    public IngredientSO ingredient;
    [Range(0f, 1f)]
    public float dropChance = 0.3f;   // 0.3 = 30%
    public int minAmount = 1;
    public int maxAmount = 1;
}

[CreateAssetMenu(menuName = "Avocado/EnemyDropTable")]
public class EnemyDropTableSO : ScriptableObject
{
    public DropEntry[] drops;
}
