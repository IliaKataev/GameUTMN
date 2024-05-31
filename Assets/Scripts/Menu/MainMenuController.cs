using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartNewGame()
    {
        // �������� ����� � ����� ����� (�������������� ��������� ����� � ������� ���������)
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        // ���������� �������� �������� (���� ������� ��������)
        Debug.Log("�������� ������� � �����������");
    }

    public void OpenResultTable()
    {
        // ���������� �������� �������� (���� ������� ��������)
        Debug.Log("�������� ������� � �����������");
    }

    public void QuitGame()
    {
        // ����� �� ����
        Application.Quit();
    }

}
