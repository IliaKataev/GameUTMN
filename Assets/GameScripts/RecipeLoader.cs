using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class RecipeLoader : MonoBehaviour
{
    public static List<DrinkRecipe> recipes;
    void Start()
    {
        string filePath = "Assets/Drinks/DIF.txt"; // ���� � ������ ����� � ���������
        RecipeReader reader = new RecipeReader();
        recipes = reader.ReadRecipesFromFile(filePath);

        // ������ ������� ��� �������� � ����� ��� � �������� ����������
    }


}
