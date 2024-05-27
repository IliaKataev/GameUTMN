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
        //foreach (var recipe in Recipes)
        //{
        //    Debug.Log("Название: " + recipe.Name);
        //    Debug.Log("Ингредиенты:");
        //    foreach (var ingredient in recipe.Ingredients)
        //    {
        //        Debug.Log(ingredient.Key + ": " + ingredient.Value);
        //    }
        //    Debug.Log("------------");
        //}
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
                    string[] ingredientInfo = ingredientData.Split(' ');
                    string ingredientName = string.Join(" ", ingredientInfo, 0, ingredientInfo.Length - 1);
                    int quantity = int.Parse(ingredientInfo[ingredientInfo.Length - 1].Trim('(', ')'));
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
