using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public Text orderText;  // UI Text component to display the order
    public string recipeFilePath = "Assets/drinks/DIF.txt";  // Path to the recipe file
    private List<DrinkRecipe> recipes;  // List to store the drink recipes
    private int currentRecipeIndex = 0;  // Index to track the current recipe
    public Text coinText;  // Text component to display coin count
    private int coinCount = 0;  // Counter for coins

    void Start()
    {
        // Load the recipes from the file when the game starts
        LoadRecipesFromFile(recipeFilePath);
        // Display the first order
        DisplayNextOrder();
        // Initialize coin display
        UpdateCoinText();
    }

    void LoadRecipesFromFile(string filePath)
    {
        recipes = new List<DrinkRecipe>();

        // Read all lines from the file
        string[] lines = System.IO.File.ReadAllLines(filePath);

        // Parse each line and create a DrinkRecipe object
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts.Length >= 2)
            {
                string name = parts[0].Trim();
                string[] ingredientsWithRatios = parts[1].Split(',');
                List<string> ingredients = new();
                List<int> ratios = new();

                foreach (string ingredientWithRatio in ingredientsWithRatios)
                {
                    string[] ingredientParts = ingredientWithRatio.Trim().Split(':');
                    if (ingredientParts.Length == 2)
                    {
                        ingredients.Add(ingredientParts[0].Trim());
                        ratios.Add(int.Parse(ingredientParts[1].Trim()));
                    }
                }

                recipes.Add(new DrinkRecipe(name, ingredients, ratios));
            }
        }
    }

    void DisplayNextOrder()
    {
        if (recipes.Count > 0)
        {
            // Get the next recipe
            DrinkRecipe recipe = recipes[currentRecipeIndex];

            // Update the UI Text with the order details
            orderText.text = $"Привет, сделай мне {recipe.Name}";

            // Increment the recipe index
            currentRecipeIndex = (currentRecipeIndex + 1) % recipes.Count;
        }
    }

    public bool CheckOrder(List<string> ingredients)
    {
        if (recipes.Count > 0)
        {
            DrinkRecipe currentRecipe = recipes[currentRecipeIndex];
            // Check if the provided ingredients match the current recipe
            if (currentRecipe.Ingredients.Count != ingredients.Count)
                return false;

            for (int i = 0; i < ingredients.Count; i++)
            {
                if (currentRecipe.Ingredients[i] != ingredients[i])
                    return false;
            }

            return true;
        }
        return false;
    }

    public void CompleteOrder()
    {
        coinCount++;
        UpdateCoinText();
        DisplayNextOrder();
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins: " + coinCount;
    }
}

