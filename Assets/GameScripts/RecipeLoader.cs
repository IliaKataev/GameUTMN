using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class RecipeLoader : MonoBehaviour
{
    void Start()
    {
        string filePath = "Assets/Drinks/DIF.txt"; // Путь к вашему файлу с рецептами
        RecipeReader reader = new RecipeReader();
        List<DrinkRecipe> recipes = reader.ReadRecipesFromFile(filePath);

        // Теперь рецепты уже выведены в дебаг лог в процессе считывания
    }
}
