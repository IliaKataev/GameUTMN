using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{

    public string ingredientName; // �������� �����������, ��������������� ������ �� ������
    public ShakerManager shakerManager; // ������ �� ��������� ShakerManager

    // ����� ���������� ��� ������� �� ������
    public void AddIngredientToShaker()
    {
        // ���������, ��� ShakerManager ��� ��������
        if (shakerManager != null)
        {
            // �������� ����� AddIngredient � ��������� �����������
            shakerManager.AddIngredient(ingredientName);
        }
    }
}
