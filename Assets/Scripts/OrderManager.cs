using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class OrderManager : MonoBehaviour
{
    private List<Order> orders = new List<Order>();

    void Start()
    {
        LoadOrdersFromFile("Assets/Drinks/DIF.txt");
    }

    void LoadOrdersFromFile(string fileName)
    {
        TextAsset file = Resources.Load<TextAsset>(fileName);
        if (file != null)
        {
            string[] lines = file.text.Split('\n');
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        string name = parts[0].Trim();
                        List<string> ingredients = new List<string>(parts[1].Trim().Split(','));
                        List<int> ratios = new List<int>();
                        foreach (string ratio in parts[2].Trim().Split(':'))
                        {
                            if (int.TryParse(ratio, out int value))
                            {
                                ratios.Add(value);
                            }
                        }
                        orders.Add(new Order(name, ingredients, ratios));
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Файл с рецептами не найден: " + fileName);
        }
    }

    public List<string> GetNewOrder()
    {
        // Логика выбора нового заказа
        int randomIndex = Random.Range(0, orders.Count);
        return orders[randomIndex].ingredients;
    }

    public string GetOrderText(List<string> ingredients)
    {
        // Формирование текста заказа
        return "Привет, я хочу: " + string.Join(", ", ingredients);
    }

    public bool IsOrderCorrect(List<string> addedIngredients)
    {
        // Проверка правильности выполнения заказа
        // Логика проверки, сравнение списков addedIngredients и currentOrder
        return true; // Поменяйте эту строку на вашу логику проверки
    }
}

[System.Serializable]
public class Order
{
    public string name;
    public List<string> ingredients;
    public List<int> ratios;

    public Order(string name, List<string> ingredients, List<int> ratios)
    {
        this.name = name;
        this.ingredients = ingredients;
        this.ratios = ratios;
    }
}
