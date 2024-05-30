using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class RecipeLoader : MonoBehaviour
{
    public List<DrinkRecipe> Recipes { get; private set; }

    void Start()
    {
        LoadRecipes("Assets/Drinks/DIF.txt");
        Debug.Log("Рецепты загружены:");
    }

    public void LoadRecipes(string path)
    {
        Recipes = new List<DrinkRecipe>();
        string[] lines = File.ReadAllLines(path, Encoding.UTF8);

        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts.Length > 1)
            {
                string name = parts[0].Trim();
                Dictionary<string, int> ingredients = new Dictionary<string, int>();

                string[] ingredientsPart = parts[1].Split(',');
                foreach (var ingredient in ingredientsPart)
                {
                    string ingredientData = ingredient.Trim();
                    int startQuote = ingredientData.IndexOf('\"') + 1;
                    int endQuote = ingredientData.LastIndexOf('\"');
                    string ingredientName = ingredientData.Substring(startQuote, endQuote - startQuote);

                    int startBracket = ingredientData.IndexOf('(') + 1;
                    int endBracket = ingredientData.IndexOf(')');
                    int quantity = int.Parse(ingredientData.Substring(startBracket, endBracket - startBracket));

                    ingredients[ingredientName] = quantity;
                }

                DrinkRecipe recipe = new DrinkRecipe(name, ingredients);
                Recipes.Add(recipe);

                // Вывод информации о рецепте в консоль
                Debug.Log("Название рецепта: " + recipe.Name);
                Debug.Log("Ингредиенты:");
                foreach (var ingredient in recipe.Ingredients)
                {
                    Debug.Log(ingredient.Key + ": " + ingredient.Value);
                }
                Debug.Log("------------");
            }
        }
    }
}
