using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highscoreText;

    public void SetHighscore(int score)
    {
        highscoreText.text = score.ToString();
    }
}
