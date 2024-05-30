using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerManager : MonoBehaviour
{
    public RecipeLoader recipeLoader; // Ссылка на компонент RecipeLoader
    public StickerManager stickerManager; // Ссылка на компонент StickerManager
    public Text orderText; // Текстовый компонент для отображения текущего заказа
    public Text resultText; // Текстовый компонент для отображения результата
    public Button resetButton; // Кнопка для сброса

    private DrinkRecipe currentOrder;
    private Dictionary<string, int> currentIngredients;
    private string currentStickerName;

    private void Start()
    {
        currentIngredients = new Dictionary<string, int>();
        Debug.Log("Текущие ингредиенты инициализированы");

        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetShaker);
        }

        GenerateNewOrder();
    }

    void GenerateNewOrder()
    {
        //1. Получение названия стикера
    }

    public void AddIngredient(string ingredient)
    {
        if (currentIngredients.ContainsKey(ingredient))
        {
            currentIngredients[ingredient]++;
        }
        else
        {
            currentIngredients[ingredient] = 1;
        }

        Debug.Log("Ингредиент добавлен: " + ingredient);
        DisplayCurrentIngredients();
    }

    public void Serve()
    {
        bool isMatch = CheckIngredients() && currentStickerName == currentOrder.Name;
        if (isMatch)
        {
            resultText.text = "Заказ выполнен!";
            stickerManager.SetCanChangeSticker(true);
            GenerateNewOrder();
            ResetShaker();
            stickerManager.ChangeSticker();
        }
        else
        {
            resultText.text = "Неверный заказ!";
        }
    }

    bool CheckIngredients()
    {
        if (currentOrder != null && currentIngredients != null)
        {
            foreach (var ingredient in currentOrder.Ingredients)
            {
                if (!currentIngredients.ContainsKey(ingredient.Key) || currentIngredients[ingredient.Key] != ingredient.Value)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            Debug.LogError("currentOrder или currentIngredients равен null.");
            return false;
        }
    }

    void ResetShaker()
    {
        currentIngredients.Clear();
        Debug.Log("Шейкер сброшен.");
        DisplayCurrentIngredients();
    }

    void DisplayCurrentIngredients()
    {
        if (currentIngredients.Count == 0)
        {
            Debug.Log("Ничего нет.");
        }
        else
        {
            Debug.Log("Текущие ингредиенты в шейкере:");
            foreach (var ingredient in currentIngredients)
            {
                Debug.Log($"{ingredient.Key}: {ingredient.Value}");
            }
        }
    }
}
