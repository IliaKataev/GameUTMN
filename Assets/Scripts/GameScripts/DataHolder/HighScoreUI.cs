using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] GameObject highscoreElementPrefab;
    [SerializeField] Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject>();

    void OnEnable()
    {
        HighscoreHandler.onHighscoreListChanged += UpdateUI;
    }

    void OnDisable()
    {
        HighscoreHandler.onHighscoreListChanged -= UpdateUI;
    }

    void ClearUI()
    {
        // ������� ������� ������ UI ���������
        foreach (var element in uiElements)
        {
            Destroy(element);
        }
        uiElements.Clear();
    }

    void UpdateUI(List<HighscoreElement> list)
    {
        ClearUI();

        // ������� ����� ������ �������� ��� ����������
        List<HighscoreElement> sortedList = new List<HighscoreElement>(list);

        // ��������� ������ �� �������� ���������� �������
        sortedList.Sort((a, b) => b.points.CompareTo(a.points));

        // ������������ ���������� ������� �� 7
        int count = Mathf.Min(sortedList.Count, 7);

        // ������� UI �������� ��� ������ ������
        for (int i = 0; i < count; i++)
        {
            HighscoreElement el = sortedList[i];

            var inst = Instantiate(highscoreElementPrefab, Vector3.zero, Quaternion.identity);
            inst.transform.SetParent(elementWrapper, false);
            inst.SetActive(true);

            // ��������� ��������� ���� � ��������� UI ��������
            var texts = inst.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = el.playerName;
            texts[1].text = el.points.ToString();

            uiElements.Add(inst);
        }
    }
}
