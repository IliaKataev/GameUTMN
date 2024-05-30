using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRulesPanel : MonoBehaviour
{
    public Button nextButton;
    public Button startButton;
    public List<Button> explanationButtons;
    public List<Sprite> explanationImages;
    public Image explanationImage;

    private int currentIndex = 0;

    void Start()
    {
        // Отобразить панель с правилами
        gameObject.SetActive(true);

        // Скрыть все кнопки объяснений
        foreach (var button in explanationButtons)
        {
            button.gameObject.SetActive(false);
        }

        // Добавить обработчик события нажатия кнопки "Следующее"
        nextButton.onClick.AddListener(ShowNextExplanation);

        // Добавить обработчик события нажатия кнопки "Начать"
        startButton.onClick.AddListener(StartGame);

        // Отобразить первое объяснение
        ShowExplanation(currentIndex);
    }

    void ShowNextExplanation()
    {
        // Отобразить следующее объяснение
        currentIndex++;
        ShowExplanation(currentIndex);

        // Если все объяснения пройдены, скрыть кнопку "Следующее" и показать кнопку "Начать"
        if (currentIndex >= explanationButtons.Count)
        {
            nextButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
        }
    }

    void ShowExplanation(int index)
    {
        // Проверка на корректность индекса
        if (index < 0 || index >= explanationButtons.Count)
        {
            return;
        }

        // Отобразить текущую кнопку объяснения
        explanationButtons[index].gameObject.SetActive(true);

        // Обновить изображение объяснения
        explanationImage.sprite = explanationImages[index];

        // Скрыть предыдущую кнопку после показа текущей
        if (index > 0)
        {
            explanationButtons[index - 1].gameObject.SetActive(false);
        }
    }

    void StartGame()
    {
        // Скрыть панель с правилами
        gameObject.SetActive(false);
    }
}