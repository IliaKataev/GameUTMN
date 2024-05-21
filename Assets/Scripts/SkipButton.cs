using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipTutorial : MonoBehaviour
{
    public void SkipToNextScene()
    {
        SceneManager.LoadScene("levels");
    }

    
}
