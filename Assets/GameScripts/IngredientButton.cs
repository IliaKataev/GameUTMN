using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    public ShakerManager shakerManager;
    public string ingredientName;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(AddIngredient);
    }

    void AddIngredient()
    {
        shakerManager.AddIngredient(ingredientName);
    }
}
