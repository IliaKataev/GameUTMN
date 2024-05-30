using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerManager : MonoBehaviour
{
    public CraftRecipe craftRecipe; //ссылка на компонент
    private List<Ingredient> shaker = new List<Ingredient>();

    public void Start()
    {
        Debug.Log("Мы зашли - это работает!");
    }

    public void AddIngredient(string ingredientName)
    {
        foreach (var recipe in craftRecipe.recipes)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                if (ingredient.NameIngredient == ingredientName)
                {
                    shaker.Add(ingredient);
                    Debug.Log("Added " + ingredientName + " to the shaker.");
                    return;
                }
            }
        }
        DisplayCurrentIngredients();
    }

    void DisplayCurrentIngredients()
    {
        if (shaker.Count == 0)
        {
            Debug.Log("Ничего нет.");
        }
        else
        {
            Debug.Log("Текущие ингредиенты в шейкере:");
            foreach (var ingredient in shaker)
            {
                Debug.Log(ingredient + ": " + ingredient);
            }
        }
    }
}
