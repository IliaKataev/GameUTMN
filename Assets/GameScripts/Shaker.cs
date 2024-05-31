using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ShakerManager : MonoBehaviour
{
    public CraftRecipe craftRecipe; //������ �� ���������
    public StickerManager stickerManager; //������ �� ���������

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
        Debug.Log("ShakerManager ���������");
        GetStickerName();

        if (serveButton != null)
        {
            serveButton.onClick.AddListener(ChangeDrinkImage);
        }
    }

    public void ChangeDrinkImage() //��� ��� �������� ������� � ���������� ����� - ������� ������
    {
        if (shaker.Count > 0 && ShakeImageOnButtonPress.isShakeButtonPressed) // ������� ���������, ��� � ������� ���� ����������� � ������ ��� ����� �������� �� ����� ������
        {
            countMakeOrder++;
            CheckIngredients();
            Debug.Log($"{coins} - ���� ����");
            Debug.Log($"{countMakeOrder} - ������, ������� �� �������");
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
                    Debug.Log("�������� " + ingredientName + " � ������");
                    return;
                }
            }
        }
    }

    public void Serve() // ��� ��� ����� ������� - ������ ����� �����
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
        Debug.Log(currentStickerName + " - �������� �������");
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
                            totalpenalty += 15; //����� �� ������� ������� ��� �� �������� 
                        }
                    }
                }
                if (totalMaxCoins < count)
                {
                    count -= totalpenalty; //�������� ���� ������ �� ���� ��� �� ����������
                    if (count < 0) count = 0;
                }
                coins += count; //���������� ���� ����
                //Debug.Log(coins + " ������������");
                //Debug.Log(totalMaxCoins + " ����� ������������");
            }
        }
    }


    void GetStickerName()
    {
        currentStickerName = stickerManager.GetCurrentStickerName();
        Debug.Log(currentStickerName + "� �������");
    }

    public void Reset()
    {
        shaker.Clear();
        Debug.Log("���� ������");
    }

    void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = $"{coins}";
        }
    }
}