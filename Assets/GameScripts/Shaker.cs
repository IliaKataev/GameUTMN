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
    private Dictionary<string, int> currentIngredients;// Словарь для хранения текущих ингредиентов
    private string currentStickerName;


    private void Start()
    {
        currentStickerName = stickerManager.GetCurrentStickerName();

        currentIngredients = new Dictionary<string, int>();
        Debug.Log("Текущие ингредиенты инициализированы");
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetShaker);
        }

        GenerateNewOrder();
    }
    //void Start()
    //{
    //    // Проверяем, правильно ли инициализирован recipeLoader
    //    if (recipeLoader == null)
    //    {
    //        Debug.LogError("RecipeLoader не правильно инициализирован.");
    //        return;
    //    }

    //    currentIngredients = new Dictionary<string, int>(); // Инициализируем currentIngredients
    //    Debug.Log("Текущие ингредиенты инициализированы");
    //    if (resetButton != null)
    //    {
    //        resetButton.onClick.AddListener(ResetShaker);
    //    }

    //    // Проверяем, что RecipeLoader был успешно инициализирован и содержит рецепты
    //    if (recipeLoader.Recipes.Count > 0)
    //    {
    //        GenerateNewOrder();
    //    }
    //    else
    //    {
    //        Debug.LogError("RecipeLoader не содержит рецептов.");
    //    }
    //}

    void GenerateNewOrder()
    {
        stickerManager.GetCurrentStickerName();
        Debug.Log("Генерате нью ордер");

        if (recipeLoader == null)
        {
            recipeLoader = FindObjectOfType<RecipeLoader>();
        }

        // Проверяем, что RecipeLoader был успешно инициализирован и содержит рецепты
        if (recipeLoader != null && recipeLoader.Recipes.Count > 0)
        {
            currentStickerName = stickerManager.GetCurrentStickerName();
            orderText.text = currentStickerName;

            Debug.Log("Найден рецепт: " + currentOrder.Name);

            // Выводим ингредиенты текущего заказа в консоль для отладки
            Debug.Log("Ингредиенты текущего заказа:");
            foreach (var ingredient in currentOrder.Ingredients)
            {
                Debug.Log(ingredient.Key + ": " + ingredient.Value);
            }

            
            stickerManager.SetCanChangeSticker(false);
        }
        else
        {
            Debug.LogError("Нет доступных рецептов или recipeLoader равен null.");
        }
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
            stickerManager.SetCanChangeSticker(true); // Разрешаем смену стикера при успешном выполнении заказа
            GenerateNewOrder();
            ResetShaker(); // Сбрасываем шейкер при успешном выполнении заказа
            stickerManager.ChangeSticker(); // Меняем стикер при успешном выполнении заказа
        }
        else
        {
            resultText.text = "Неверный заказ!";
            // Добавьте анимацию встряхивания здесь, если необходимо
        }
    }

    bool CheckIngredients()
    {
        if (currentOrder != null && currentIngredients != null)
        {
            Dictionary<string, int> dp = new Dictionary<string, int>();
            foreach (var ingredient in currentOrder.Ingredients)
            {
                if (!currentIngredients.ContainsKey(ingredient.Key) || currentIngredients[ingredient.Key] != ingredient.Value)
                {
                    return false;
                }
                dp[ingredient.Key] = currentIngredients[ingredient.Key];
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
                Debug.Log(ingredient.Key + ": " + ingredient.Value);
            }
        }
    }
}
