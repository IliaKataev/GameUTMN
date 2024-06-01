using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{ 
    // Метод для перезапуска уровня
    public void ReastartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Метод для перехода на уровень выбора
    public void ChangeScene_1()
    {
        SceneManager.LoadScene("levels");
    }

    // Метод для перехода на уровень BeginnerMD
    public void ChangeScene_Begginer()
    {
        SceneManager.LoadScene("BeginnerMD");
    }
}
