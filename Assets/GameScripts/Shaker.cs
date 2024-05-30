using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerManager : MonoBehaviour
{
    public CraftRecipe craftRecipe; //������ �� ���������
    public StickerManager stickerManager; //������ �� ���������

    private List<Ingredient> shaker = new List<Ingredient>();
    private string currentStickerName = StickerManager.currentStickerName;
    private int coins;

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
    }

    public void Serve()
    {
        if (CheckIngredients())
        {
            Debug.Log("��� �����!");
            //�������� ����
            Reset();
        }
        else
        {
            Debug.Log("��� �� �����!");
            Reset();
        }
        stickerManager.SetRandomSticker();
        stickerManager.DisplayRecipe();
    }

    bool CheckIngredients()
    {
        foreach (var recipe in craftRecipe.recipes)
        {
            if (currentStickerName == recipe.NameRecipe)
            {
                if (recipe.Ingredients.Count != shaker.Count) // ��������� �������� ���������� ������������
                {
                    return false;
                }

                foreach (var ingredientTrue in recipe.Ingredients)
                {
                    bool ingredientFound = false;

                    foreach (var ingredientShaker in shaker)
                    {
                        if (ingredientShaker.NameIngredient == ingredientTrue.NameIngredient && 
                            ingredientShaker.CountIngredient == ingredientTrue.CountIngredient) 
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        return false;
    }


    /*
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
                    Debug.Log(ingredient.NameIngredient + ": " + ingredient.CountIngredient);
                }
            }
        }*/

    public void Reset()
    {
        shaker.Clear();
        Debug.Log("���� ������");
    }
}
