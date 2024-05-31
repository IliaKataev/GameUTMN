using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class TimerScript : MonoBehaviour
{
    public Image timerBar;
    public float maxTime = 5f; // Set this to 600 for 10 minutes
    float timeLeft;
    public GameObject timesUpText;
    public Image fadeImage; // UI Image object to handle the fade effect
    public Button startButton; // —сылка на кнопку запуска
    public Button pauseButton; // —сылка на кнопку паузы
    public Button resumeButton; // —сылка на кнопку возобновлени€
    private bool isTimerRunning = false;
    private bool isTimerPaused = false;
    public TMP_Text coinstext;
    public GameObject Panel_end;
    public Image GameOver;
    public Sprite Goodgame;
    public Sprite Badgame;

    // Start is called before the first frame update
    void Start()
    {
        timesUpText.SetActive(false);
        fadeImage.gameObject.SetActive(false); // Hide the fade image initially
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;


        if (startButton != null)
        {
            startButton.onClick.AddListener(StartTimer);
        }
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseTimer);
        }
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeTimer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning && !isTimerPaused && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else if (timeLeft <= 0)
        {
            timesUpText.SetActive(true);
            StartCoroutine(EndDayRoutine());
        }
    }

    IEnumerator EndDayRoutine()
    {
        yield return new WaitForSeconds(2); // Display "The day is over" text for 2 seconds
        StartCoroutine(FadeToBlack());
        ShowResults();

    }

    void ShowResults()
    {
        Panel_end.SetActive(true);

        ShakerManager shakerManager =FindAnyObjectByType<ShakerManager>();
        if (shakerManager!=null)
        {
            coinstext.text= $"{ShakerManager.coins}";
            if (ShakerManager.coins >= 100)
            {
                GameOver.sprite =Goodgame;
            }
            else { GameOver.sprite = Badgame; }
        }

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
        //SceneManager.LoadScene("MainMenu"); // Assuming you have a MainMenu scene
    }

    void StartTimer()
    {
        isTimerRunning = true;
        isTimerPaused = false;
    }

    void PauseTimer()
    {
        isTimerPaused = true;
    }

    void ResumeTimer()
    {
        isTimerPaused = false;
    }
}