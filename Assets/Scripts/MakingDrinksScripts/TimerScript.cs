using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 5f; // Set this to 600 for 10 minutes
    float timeLeft;
    public GameObject timesUpText;
    public Image fadeImage; // UI Image object to handle the fade effect

    // Start is called before the first frame update
    void Start()
    {
        timesUpText.SetActive(false);
        fadeImage.gameObject.SetActive(false); // Hide the fade image initially
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            timesUpText.SetActive(true);
            StartCoroutine(EndDayRoutine());
        }
    }

    IEnumerator EndDayRoutine()
    {
        yield return new WaitForSeconds(2); // Display "The day is over" text for 2 seconds
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        fadeImage.gameObject.SetActive(true);
        Color fadeColor = fadeImage.color;
        float fadeDuration = 2.0f; // Duration of the fade effect

        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            fadeColor.a = Mathf.Lerp(0, 1, Mathf.Min(1, t / fadeDuration));
            fadeImage.color = fadeColor;
            yield return null;
        }

        Time.timeScale = 1; // Reset time scale before changing the scene
        SceneManager.LoadScene("MainMenu"); // Assuming you have a MainMenu scene
    }
}