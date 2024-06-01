using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShakerManager : MonoBehaviour
{
    public CraftRecipe craftRecipe; //������ �� ���������
    public StickerManager stickerManager; //������ �� ���������

    public static string playerName;

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


    public HighscoreHandler highscoreHandler;

    void Start()
    {
        Debug.Log("ShakerManager ���������");
        GetStickerName();

        if (serveButton != null)
        {
            serveButton.onClick.AddListener(Serve);
        }
    }

    void Serve() //��� ��� �������� ������� � ���������� ����� - ������� ������
    {
        if (shaker.Count > 0 && ShakeImageOnButtonPress.isShakeButtonPressed) // ������� ���������, ��� � ������� ���� ����������� � ������ ��� ����� �������� �� ����� ������
        {
            countMakeOrder++;
            CheckIngredients();
            Debug.Log($"{coins} - ���� ����");
            Debug.Log($"{countMakeOrder} - ������, ������� �� �������");
            SetDrinkImage();
            UpdateCoinsText();
            isResetFromServe = true;
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
        else
        {   
            if(shaker.Count > 0) 
            {
                // ������ ��� ������ � ��������� ������
                FindObjectOfType<ShakeButtonEffect>().TriggerShakeAndHighlight();
            }
            else
            {
                if (shaker.Count == 0)
                {
                    FindObjectOfType<ShakePanelEffect>().TriggerShake();
                }
                
            }
            
        }
    }

    IEnumerator ShowPanelWithDelay()
    {
        yield return new WaitForSeconds(1); // ���� 1 �������
        Panel.SetActive(true); // ���������� ������ ����� ��������
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
        // ���������, �������� �� ������� ������� ������� - ��� �����, ����� �� �� ������ ����������, ���� ��� ������� �����
        bool isDrinkReady = false;
        foreach (var recipe in craftRecipe.resultsDrink)
        {
            if (currentStickerName == recipe.NameRecipe)
            {

                if (drinkImage.sprite == recipe.StickerImage)
                {
                    isDrinkReady = true;
                    break;
                }
            }
        }

        if (!isDrinkReady) //�� ���� ���� � ��� ������ �������, �� ���������
        {
            // ��������� ���������� � ������
            foreach (var recipe in craftRecipe.recipes)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    if (ingredient.NameIngredient == ingredientName)
                    {
                        shaker.Add(ingredient);
                        Debug.Log("�������� " + ingredientName + " � ������");
                        ShakeImageOnButtonPress.isShakeButtonPressed = false;
                        return;
                    }
                }
            }
        }
        else //���� � ��� ������ ������� ��������, �� ������ ������ ��������
        {
            Debug.Log("������ �������� ����������, ������� �����");
        }
    }

    public void ChangeDrinkImage() // ��� ��� ����� ������� - ������ ����� �����
    {
        if (drinkImage != null && originalDrinkImageSprite != null)
        {
            drinkImage.sprite = originalDrinkImageSprite;
        }

        stickerManager.SetRandomSticker();
        GetStickerName();
        stickerManager.DisplayRecipe();
    }

    void SetDrinkImage() //��������� �� ������ �������� �������
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
                            isGoodDrink = true;
                            Debug.Log($"����� - {ingredientShaker.NameIngredient}");
                        }
                        else
                        {
                            totalpenalty += 15; //����� �� ������� ������� ��� �� �������� 
                            Debug.Log($"�� ����� - {ingredientShaker.NameIngredient}");
                        }
                    }
                }
                if (totalMaxCoins < count)
                {
                    if (totalpenalty > 0) { isGoodDrink = false; }
                    count -= totalpenalty; //�������� ���� ������ �� ���� ��� �� ����������
                    if (count < 0) count = 0;
                }
                coins += count; //���������� ���� ����
                //Debug.Log(coins + " ������������");
                //Debug.Log(totalMaxCoins + " ����� ������������");
            }
        }
        UpdateHighscore();
    }

    public void UpdateHighscore() // ���������
    {
        if (highscoreHandler != null)
        {
            highscoreHandler.AddHighscoresIfPossible(new HighscoreElement(playerName, coins));
        }
        else
        {
            Debug.LogWarning("HighscoreHandler not attached to an object");
        }
    }

    void GetStickerName()
    {
        currentStickerName = stickerManager.GetCurrentStickerName();
        Debug.Log(currentStickerName + "� �������");
    }

    private bool isResetFromServe = false;

    public void Reset()
    {
        if (!isResetFromServe && shaker.Count > 0)
        {
            // ��������� �������� �������
            FindObjectOfType<ShakerResetAnimation>().StartShakerAnimation();
        }
        else
        {
            if(shaker.Count == 0) { FindObjectOfType<ShakePanelEffect>().TriggerShake(); }
            
        }

        shaker.Clear();
        Debug.Log("���� ������");
        isResetFromServe = false; // ���������� ���� ����� ���������� ������
    }

    void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = $"{coins}";
        }
    }
}