using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;



public class DrinkRecipe
{
    public string Name { get; set; }
    public Dictionary<string, int> Ingredients { get; set; }

    public DrinkRecipe(string name, Dictionary<string,int> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }

    public void Display()
    {
        Console.WriteLine($"Recipe Name: {Name}");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in Ingredients)
        {
            Console.WriteLine($"- {ingredient.Key}: {ingredient.Value}");
        }
        Console.WriteLine();
    }
}


public class RecipeReader
{
    public List<DrinkRecipe> ReadRecipesFromFile(string filePath)
    {
        List<DrinkRecipe> recipes = new List<DrinkRecipe>();

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    string name = parts[0].Trim();
                    string[] ingredientPairs = parts[1].Trim().Split(',');

                    Dictionary<string, int> ingredients = new Dictionary<string, int>();
                    foreach (var pair in ingredientPairs)
                    {
                        string[] ingredient = pair.Trim().Split('(');
                        string ingredientName = ingredient[0].Trim();
                        int quantity = int.Parse(ingredient[1].Replace(")", "").Trim());
                        ingredients.Add(ingredientName, quantity);
                    }

                    DrinkRecipe recipe = new DrinkRecipe(name, ingredients);
                    recipes.Add(recipe);
                    recipe.Display(); // Выводим информацию о рецепте в дебаг лог
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error reading file: " + ex.Message);
        }

        return recipes;
    }
    public static DrinkRecipe GetRecipeByStickerName(List<DrinkRecipe> recipes, string stickerName)
    {
        // Предположим, что название стикера соответствует названию рецепта
        foreach (var recipe in recipes)
        {
            if (recipe.Name == stickerName)
            {
                return recipe;
            }
        }
        // Вернуть null, если рецепт не найден
        return null;
    }


}

