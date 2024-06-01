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
        // Очищаем текущий список UI элементов
        foreach (var element in uiElements)
        {
            Destroy(element);
        }
        uiElements.Clear();
    }

    void UpdateUI(List<HighscoreElement> list)
    {
        ClearUI();

        // Создаем копию списка рекордов для сортировки
        List<HighscoreElement> sortedList = new List<HighscoreElement>(list);

        // Сортируем список по убыванию количества монеток
        sortedList.Sort((a, b) => b.points.CompareTo(a.points));

        // Ограничиваем количество записей до 7
        int count = Mathf.Min(sortedList.Count, 7);

        // Создаем UI элементы для каждой записи
        for (int i = 0; i < count; i++)
        {
            HighscoreElement el = sortedList[i];

            var inst = Instantiate(highscoreElementPrefab, Vector3.zero, Quaternion.identity);
            inst.transform.SetParent(elementWrapper, false);
            inst.SetActive(true);

            // Обновляем текстовые поля в созданном UI элементе
            var texts = inst.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = el.playerName;
            texts[1].text = el.points.ToString();

            uiElements.Add(inst);
        }
    }
}
