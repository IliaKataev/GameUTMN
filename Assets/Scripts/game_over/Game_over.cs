using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{



    public void ReastartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ChangeScene_1()
    {
        SceneManager.LoadScene("levels");
    }

    public void ChangeScene_Begginer()
    {
        SceneManager.LoadScene("BeginnerMD");
    }
}
