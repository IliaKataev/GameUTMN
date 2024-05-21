using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTutorialButton : MonoBehaviour
{
    public void TutorialButton()
    {
        SceneManager.LoadScene("TrainMD");
    }

    public void BegginerButton()
    {
        SceneManager.LoadScene("BeginnerMD");
    }

    public void ProButton()
    {
        SceneManager.LoadScene("ProMD");
    }
}
