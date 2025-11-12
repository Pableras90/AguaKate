using UnityEngine;
using TMPro;
using System.Text;

public class InventoryHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private bool subscribed;

    private void OnEnable()
    {
        Debug.Log("[HUD] OnEnable()");
        TrySubscribe();
        Refresh(); // pinta estado inicial
    }

    private void Update()
    {
        // Si aún no pudo suscribirse porque Instance no existía, reintenta
        if (!subscribed) TrySubscribe();
    }

    private void OnDisable()
    {
        Debug.Log("[HUD] OnDisable()");
        if (IngredientInventory.Instance != null)
            IngredientInventory.Instance.OnInventoryChanged -= Refresh;
        subscribed = false;
    }

    private void TrySubscribe()
    {
        if (IngredientInventory.Instance == null) return;

        // evita doble suscripción
        IngredientInventory.Instance.OnInventoryChanged -= Refresh;
        IngredientInventory.Instance.OnInventoryChanged += Refresh;
        subscribed = true;

        Debug.Log($"[HUD] Subscribed to inventory InstanceID={IngredientInventory.Instance.GetInstanceID()}");
    }

    private void Refresh()
    {
        Debug.Log($"[HUD] Refresh()  InstanceID={IngredientInventory.Instance?.GetInstanceID()}");

        if (text == null || IngredientInventory.Instance == null) return;

        var dict = IngredientInventory.Instance.GetAll();
        if (dict.Count == 0)
        {
            text.text = "(sin ingredientes)";
            return;
        }

        var sb = new StringBuilder();
        foreach (var kv in dict)
            sb.AppendLine($"{kv.Key.ingredientName}: {kv.Value}");

        text.text = sb.ToString();
    }
}
