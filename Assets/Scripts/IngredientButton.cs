using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{

    public string ingredientName; // Название ингредиента, соответствующее тексту на кнопке
    public ShakerManager shakerManager; // Ссылка на экземпляр ShakerManager

    // Метод вызывается при нажатии на кнопку
    public void AddIngredientToShaker()
    {
        // Проверяем, что ShakerManager был присвоен
        if (shakerManager != null)
        {
            // Вызываем метод AddIngredient с названием ингредиента
            shakerManager.AddIngredient(ingredientName);
        }
    }
}
