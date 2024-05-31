using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ShakerManager : MonoBehaviour
{
    public CraftRecipe craftRecipe; //ссылка на компонент
    public StickerManager stickerManager; //ссылка на компонент

    private List<Ingredient> shaker = new List<Ingredient>();
    private static int coins;
    private string currentStickerName;
    private int countMakeOrder;

    public TMP_Text coinsText;
    public Button serveButton;
    public Image drinkImage;
    public Sprite originalDrinkImageSprite;

    void Start()
    {
        Debug.Log("ShakerManager стартовал");
        GetStickerName();

        if (serveButton != null)
        {
            serveButton.onClick.AddListener(ChangeDrinkImage);
        }
    }

    public void ChangeDrinkImage() //это для проверки напитка и добавление очков - кнопнка подать
    {
        if (shaker.Count > 0 && ShakeImageOnButtonPress.isShakeButtonPressed) // Условие проверяет, что в шейкере есть ингредиенты и только при такой ситуации мы можем подать
        {
            countMakeOrder++;
            CheckIngredients();
            Debug.Log($"{coins} - наши очки");
            Debug.Log($"{countMakeOrder} - заказы, которые мы сделали");
            SetDrinkImage();
            UpdateCoinsText();
            Reset();
            ShakeImageOnButtonPress.isShakeButtonPressed = false;
        }
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
                    Debug.Log("Добавлен " + ingredientName + " в шейкер");
                    return;
                }
            }
        }
    }

    public void Serve() // это для смены стикера - кнопка новый заказ
    {
        if (drinkImage != null && originalDrinkImageSprite != null)
        {
            drinkImage.sprite = originalDrinkImageSprite;
        }

        stickerManager.SetRandomSticker();
        GetStickerName();
        stickerManager.DisplayRecipe();
    }

    void SetDrinkImage()
    {

        foreach (var recipe in craftRecipe.resultsDrink)
        {
            if (currentStickerName == recipe.NameRecipe)
            {

                if (drinkImage != null)
                {
                    drinkImage.sprite = recipe.StickerImage;
                }
                break;
            }
        }
    }

    void CheckIngredients()
    {
        Debug.Log(currentStickerName + " - название стикера");
        int totalMaxCoins = 0;
        int totalpenalty = 0;
        foreach (var recipe in craftRecipe.recipes)
        {
            if (currentStickerName == recipe.NameRecipe)
            {
                int maxCoins = 0;
                int count = 0;
                foreach (var ingredientTrue in recipe.Ingredients)
                {
                    maxCoins = ingredientTrue.CountIngredient * 15;
                    totalMaxCoins += maxCoins;

                    foreach (var ingredientShaker in shaker)
                    {
                        if (ingredientShaker.NameIngredient == ingredientTrue.NameIngredient) 
                        {
                            count += 15;
                        }
                        else
                        {
                            totalpenalty += 15; //здесь он считает сколько раз мы ошиблись 
                        }
                    }
                }
                if (totalMaxCoins < count)
                {
                    count -= totalpenalty; //вычитает нашу ошибку из того что мы заработали
                    if (count < 0) count = 0;
                }
                coins += count; //прибавляет наши очки
                //Debug.Log(coins + " заработанное");
                //Debug.Log(totalMaxCoins + " общее максимальное");
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

    void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = $"{coins}";
        }
    }
}