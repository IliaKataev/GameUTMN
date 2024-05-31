using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShakerManager : MonoBehaviour
{
    public CraftRecipe craftRecipe; //ссылка на компонент
    public StickerManager stickerManager; //ссылка на компонент

    private List<Ingredient> shaker = new List<Ingredient>();
    public static int coins;
    private string currentStickerName;
    private int countMakeOrder;

    public TMP_Text coinsText;
    public Button serveButton;
    public Image drinkImage;
    public Sprite originalDrinkImageSprite;

    public Button nextButtonTrain;
    public GameObject Panel;
    public Image GameOver;
    public Sprite Goodgame;
    public Sprite Badgame;
    private bool isGoodDrink = false;

    void Start()
    {
        Debug.Log("ShakerManager стартовал");
        GetStickerName();

        if (serveButton != null)
        {
            serveButton.onClick.AddListener(Serve);
        }
    }

    void Serve() //это дл€ проверки напитка и добавление очков - кнопнка подать
    {
        if (shaker.Count > 0 && ShakeImageOnButtonPress.isShakeButtonPressed) // ”словие провер€ет, что в шейкере есть ингредиенты и только при такой ситуации мы можем подать
        {
            countMakeOrder++;
            CheckIngredients();
            Debug.Log($"{coins} - наши очки");
            Debug.Log($"{countMakeOrder} - заказы, которые мы сделали");
            SetDrinkImage();
            UpdateCoinsText();
            Reset();
            ShakeImageOnButtonPress.isShakeButtonPressed = false;
            if (SceneManager.GetActiveScene().name == "TrainMD")
            {
                if (countMakeOrder == 1)
                {
                    StartCoroutine(ShowPanelWithDelay());
                }
            }
        }
    }

    IEnumerator ShowPanelWithDelay()
    {
        yield return new WaitForSeconds(1); // ∆дем 1 секунды
        Panel.SetActive(true); // јктивируем панель после задержки
        if (isGoodDrink)
        {
            GameOver.sprite = Goodgame;
        }
        else 
        {
            GameOver.sprite = Badgame;
            nextButtonTrain.gameObject.SetActive(false);
        }
    }

    public void AddIngredient(string ingredientName)
    {
        // ѕровер€ем, €вл€етс€ ли текущий напиток готовым - это нужно, чтобы мы не ложили игредиенты, если наш напиток готов
        bool isDrinkReady = false;
        foreach (var recipe in craftRecipe.resultsDrink)
        {
            if (currentStickerName == recipe.NameRecipe)
            {

                if (drinkImage.sprite == recipe.StickerImage)
                {
                    isDrinkReady = true; break;
                }
            }
        }

        if (!isDrinkReady) //по сути если у нас спрайт шейкера, то добавл€ем
        {
            // ƒобавл€ем ингредиент в шейкер
            foreach (var recipe in craftRecipe.recipes)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    if (ingredient.NameIngredient == ingredientName)
                    {
                        shaker.Add(ingredient);
                        Debug.Log("ƒобавлен " + ingredientName + " в шейкер");
                        return;
                    }
                }
            }
        }
        else //если у нас спрайт напитка готового, то нельз€ ничего добавить
        {
            Debug.Log("Ќельз€ добавить ингредиент, напиток готов");
        }
    }

    public void ChangeDrinkImage() // это дл€ смены стикера - кнопка новый заказ
    {
        if (drinkImage != null && originalDrinkImageSprite != null)
        {
            drinkImage.sprite = originalDrinkImageSprite;
        }

        stickerManager.SetRandomSticker();
        GetStickerName();
        stickerManager.DisplayRecipe();
    }

    void SetDrinkImage() //изменение на спрайт готового напитка
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
                            isGoodDrink = true;
                            Debug.Log($"верно - {ingredientShaker.NameIngredient}");
                        }
                        else
                        {
                            totalpenalty += 15; //здесь он считает сколько раз мы ошиблись 
                            Debug.Log($"Ќе верно - {ingredientShaker.NameIngredient}");
                        }
                    }
                }
                if (totalMaxCoins < count)
                {
                    if (totalpenalty > 0) { isGoodDrink = false; }
                    count -= totalpenalty; //вычитает нашу ошибку из того что мы заработали
                    if (count < 0) count = 0;
                }
                coins += count; //прибавл€ет наши очки
                //Debug.Log(coins + " заработанное");
                //Debug.Log(totalMaxCoins + " общее максимальное");
            }
        }
    }


    void GetStickerName()
    {
        currentStickerName = stickerManager.GetCurrentStickerName();
        Debug.Log(currentStickerName + "¬ шейкере");
    }

    public void Reset()
    {
        shaker.Clear();
        Debug.Log("ѕуст шейкер");
    }

    void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = $"{coins}";
        }
    }
}