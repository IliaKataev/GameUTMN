using System.Collections.Generic;
using UnityEngine;
using System;

public class ShakerManager : MonoBehaviour
{
    public CraftRecipe craftRecipe; //ссылка на компонент
    public StickerManager stickerManager; //ссылка на компонент

    private List<Ingredient> shaker = new List<Ingredient>();
    private static int coins;
    private string currentStickerName;

    void Start()
    {
        Debug.Log("ShakerManager стартовал");
        GetStickerName();
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
    }

    public void Serve()
    {
        CheckIngredients();
        Debug.Log(coins);
        Reset();
        stickerManager.SetRandomSticker();
        GetStickerName();
        stickerManager.DisplayRecipe();
    }

    void CheckIngredients()
    {
        Debug.Log(currentStickerName + " название стикера");
        foreach (var recipe in craftRecipe.recipes)
        {
            if (currentStickerName == recipe.NameRecipe)
            {
                int maxCoins = 0;
                int totalMaxCoins = 0;
                int count = 0 ;
                foreach (var ingredientTrue in recipe.Ingredients)
                {
                    maxCoins = ingredientTrue.CountIngredient;
                    totalMaxCoins += maxCoins;

                    foreach (var ingredientShaker in shaker)
                    {
                        if (ingredientShaker.NameIngredient == ingredientTrue.NameIngredient) // сок
                        {
                            count += 1;
                        }
                    }
                }
                coins += count;
                Debug.Log(coins + " заработанное");
                Debug.Log(totalMaxCoins + " общее максимальное");
            }
        }
    }


    void GetStickerName()
    {
        currentStickerName = stickerManager.GetCurrentStickerName();
        Debug.Log(currentStickerName + "В шейкере");
    }

    public void Reset()
    {
        shaker.Clear();
        Debug.Log("Пуст шейкер");
    }
}


