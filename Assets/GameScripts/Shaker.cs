using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerManager : MonoBehaviour
{
    public RecipeLoader recipeLoader; // ������ �� ��������� RecipeLoader
    public StickerManager stickerManager; // ������ �� ��������� StickerManager
    public Text orderText; // ��������� ��������� ��� ����������� �������� ������
    public Text resultText; // ��������� ��������� ��� ����������� ����������
    public Button resetButton; // ������ ��� ������

    private DrinkRecipe currentOrder;
    private Dictionary<string, int> currentIngredients;
    private string currentStickerName;

    private void Start()
    {
        currentIngredients = new Dictionary<string, int>();
        Debug.Log("������� ����������� ����������������");

        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetShaker);
        }

        GenerateNewOrder();
    }

    void GenerateNewOrder()
    {
        string currentStickerName = stickerManager.GetCurrentStickerName(); // �������� �������� �������� �������
        currentOrder = RecipeReader.GetRecipeByStickerName(RecipeLoader.recipes,currentStickerName); // ���� ������ �� �������� �������

        if (currentOrder != null)
        {
            orderText.text = currentOrder.Name; // ������� �������� ������ �� �����
            Debug.Log("������ ������: " + currentOrder.Name);

            // ������� ����������� �������� ������ � ����� ���
            Debug.Log("����������� �������� ������:");
            foreach (var ingredient in currentOrder.Ingredients)
            {
                Debug.Log($"{ingredient.Key}: {ingredient.Value}");
            }

            stickerManager.SetCanChangeSticker(false); // ��������� ����� �������
        }
        else
        {
            Debug.LogError("������ ��� ������� " + currentStickerName + " �� ������.");
        }
    }

    public void AddIngredient(string ingredient)
    {
        if (currentIngredients.ContainsKey(ingredient))
        {
            currentIngredients[ingredient]++;
        }
        else
        {
            currentIngredients[ingredient] = 1;
        }

        Debug.Log("���������� ��������: " + ingredient);
        DisplayCurrentIngredients();
    }

    public void Serve()
    {
        bool isMatch = CheckIngredients() && currentStickerName == currentOrder.Name;
        if (isMatch)
        {
            resultText.text = "����� ��������!";
            stickerManager.SetCanChangeSticker(true);
            GenerateNewOrder();
            ResetShaker();
            stickerManager.ChangeSticker();
        }
        else
        {
            resultText.text = "�������� �����!";
        }
    }

    bool CheckIngredients()
    {
        if (currentOrder != null && currentIngredients != null)
        {
            foreach (var ingredient in currentOrder.Ingredients)
            {
                if (!currentIngredients.ContainsKey(ingredient.Key) || currentIngredients[ingredient.Key] != ingredient.Value)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            Debug.LogError("currentOrder ��� currentIngredients ����� null.");
            return false;
        }
    }

    void ResetShaker()
    {
        currentIngredients.Clear();
        Debug.Log("������ �������.");
        DisplayCurrentIngredients();
    }

    void DisplayCurrentIngredients()
    {
        if (currentIngredients.Count == 0)
        {
            Debug.Log("������ ���.");
        }
        else
        {
            Debug.Log("������� ����������� � �������:");
            foreach (var ingredient in currentIngredients)
            {
                Debug.Log($"{ingredient.Key}: {ingredient.Value}");
            }
        }
    }
}