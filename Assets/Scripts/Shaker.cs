using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shaker : MonoBehaviour
{
    public Image shakerImage;
    public List<Image> ingredientLayers;
    public Text orderText;
    public Button resetButton;
    public Button completeButton;
    public OrderManager orderManager;

    private List<string> currentOrder;
    private List<string> addedIngredients = new List<string>();

    void Start()
    {
        resetButton.onClick.AddListener(ResetShaker);
        completeButton.onClick.AddListener(CheckOrder);
        LoadNewOrder();
    }

    public void AddIngredient(string ingredient)
    {
        if (addedIngredients.Count < 4)
        {
            addedIngredients.Add(ingredient);
            ingredientLayers[addedIngredients.Count - 1].sprite = Resources.Load<Sprite>($"Sprites/{ingredient}");
            ingredientLayers[addedIngredients.Count - 1].enabled = true;
        }
    }

    public void ResetShaker()
    {
        addedIngredients.Clear();
        foreach (var layer in ingredientLayers)
        {
            layer.enabled = false;
        }
    }

    public void CheckOrder()
    {
        if (orderManager.IsOrderCorrect(addedIngredients))
        {
            StartCoroutine(ShowSuccess());
        }
        else
        {
            ResetShaker();
        }
    }

    private IEnumerator ShowSuccess()
    {
        // Показать сообщение о выполнении заказа
        completeButton.interactable = false;
        yield return new WaitForSeconds(2f); // Время показа сообщения
        completeButton.interactable = true;
        LoadNewOrder();
    }

    private void LoadNewOrder()
    {
        ResetShaker();
        currentOrder = orderManager.GetNewOrder();
        orderText.text = orderManager.GetOrderText(currentOrder);
    }
}
