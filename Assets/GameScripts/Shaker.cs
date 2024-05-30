using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerManager : MonoBehaviour
{
    public CraftRecipe craftRecipe; //������ �� ���������
    private List<Ingredient> shaker = new List<Ingredient>();

    public void Start()
    {
        Debug.Log("�� ����� - ��� ��������!");
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
            Debug.Log("������ ���.");
        }
        else
        {
            Debug.Log("������� ����������� � �������:");
            foreach (var ingredient in shaker)
            {
                Debug.Log(ingredient + ": " + ingredient);
            }
        }
    }
}
