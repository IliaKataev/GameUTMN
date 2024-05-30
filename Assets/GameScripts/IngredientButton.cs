using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    public ShakerManager shakerManager;
    public Button button;

    void Start()
    {
        button.onClick.AddListener(() => AddIngredient(button.name));
    }

    void AddIngredient(string ingredientName)
    {
        shakerManager.AddIngredient(ingredientName);
    }
}