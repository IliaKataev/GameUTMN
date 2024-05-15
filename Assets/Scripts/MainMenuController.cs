using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartNewGame()
    {
        // Загрузка сцены с новой игрой (предварительно созданную сцену с игровым процессом)
        SceneManager.LoadScene("Bar");
    }

    public void LoadGame()
    {
        // Реализация загрузки сохранений (пока оставим заглушку)
        Debug.Log("Открытие оверлея с сохранениями или сообщение 'Начните новую игру!'");
    }

    public void OpenSettings()
    {
        // Реализация открытия настроек (пока оставим заглушку)
        Debug.Log("Открытие оверлея с настройками");
    }

    public void QuitGame()
    {
        // Выход из игры
        Application.Quit();
    }
}
