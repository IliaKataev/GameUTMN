using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class RecipeLoader : MonoBehaviour
{
    void Start()
    {
        string filePath = "Assets/Drinks/DIF.txt"; // ���� � ������ ����� � ���������
        RecipeReader reader = new RecipeReader();
        List<DrinkRecipe> recipes = reader.ReadRecipesFromFile(filePath);

        // ������ ������� ��� �������� � ����� ��� � �������� ����������
    }
}
