using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public void changeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
