using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int countScene;

    public void changeScene(int countScene)
    {
        SceneManager.LoadScene(countScene);
    }
}
