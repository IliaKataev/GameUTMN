using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighscoreHandler : MonoBehaviour
{
    List<HighscoreElement> highscoreList = new List<HighscoreElement>();
    [SerializeField] int maxCount = 7;
    [SerializeField] string filename;

    public delegate void OnHighscoreListChanged(List<HighscoreElement> list);
    public static event OnHighscoreListChanged onHighscoreListChanged;

    private void Start()
    {
        LoadHighscores();
    }
    private void LoadHighscores()
    {
        highscoreList = FileHandler.ReadListFromJSON<HighscoreElement>(filename);

        while(highscoreList.Count > maxCount)
        {
            highscoreList.RemoveAt(maxCount);
        }

        if(onHighscoreListChanged != null)
        if (onHighscoreListChanged != null)
        {
            onHighscoreListChanged.Invoke(highscoreList);
        }

        Debug.Log("Highscore list loaded. Count: " + highscoreList.Count);
        foreach (var entry in highscoreList)
        {
            Debug.Log("Player: " + entry.playerName + ", Points: " + entry.points);
        }
    }
    private void SavingHighscores()
    {
        FileHandler.SaveToJSON<HighscoreElement>(highscoreList, filename);
    }

    public void AddHighscoresIfPossible(HighscoreElement element)
    {
        {
            if(i >= highscoreList.Count || element.points > highscoreList[i].points)
            {
                highscoreList.Insert(i, element);

                while (highscoreList.Count > maxCount)
                {
                    highscoreList.RemoveAt(maxCount);
                }

                SavingHighscores();

                if (onHighscoreListChanged != null)
                {
                    onHighscoreListChanged.Invoke(highscoreList);
                }
                break;
            }
        }
    }
}
