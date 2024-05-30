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
        // ���������� ������ � ���������
        gameObject.SetActive(true);

        // ������ ��� ������ ����������
        foreach (var button in explanationButtons)
        {
            button.gameObject.SetActive(false);
        }

        // �������� ���������� ������� ������� ������ "���������"
        nextButton.onClick.AddListener(ShowNextExplanation);

        // �������� ���������� ������� ������� ������ "������"
        startButton.onClick.AddListener(StartGame);

        // ���������� ������ ����������
        ShowExplanation(currentIndex);
    }

    void ShowNextExplanation()
    {
        // ���������� ��������� ����������
        currentIndex++;
        ShowExplanation(currentIndex);

        // ���� ��� ���������� ��������, ������ ������ "���������" � �������� ������ "������"
        if (currentIndex >= explanationButtons.Count)
        {
            nextButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
        }
    }

    void ShowExplanation(int index)
    {
        // �������� �� ������������ �������
        if (index < 0 || index >= explanationButtons.Count)
        {
            return;
        }

        // ���������� ������� ������ ����������
        explanationButtons[index].gameObject.SetActive(true);

        // �������� ����������� ����������
        explanationImage.sprite = explanationImages[index];

        // ������ ���������� ������ ����� ������ �������
        if (index > 0)
        {
            explanationButtons[index - 1].gameObject.SetActive(false);
        }
    }

    void StartGame()
    {
        // ������ ������ � ���������
        gameObject.SetActive(false);
    }
}