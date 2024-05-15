using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.1f;
    public string dialogueFilePath = "Assets/bartender/BIM.txt";

    private string[] dialogues;
    private int currentDialogueIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        // Прочитайте реплики из файла
        LoadDialoguesFromFile(dialogueFilePath);
        // Начните отображение первой реплики
        StartCoroutine(DisplayNextDialogue());
    }

    void Update()
    {
        // Если текст еще не отображается и нажата кнопка мыши
        if (!isTyping && Input.GetMouseButtonDown(0))
        {
            // Перейдите к следующей реплике или завершите диалог, если это была последняя реплика
            if (currentDialogueIndex < dialogues.Length - 1)
            {
                currentDialogueIndex++;
                StartCoroutine(DisplayNextDialogue());
            }
            else
            {
                // Завершение диалога
                EndDialogue();
            }
        }
    }

    IEnumerator DisplayNextDialogue()
    {
        isTyping = true;
        dialogueText.text = ""; // Очистить текст перед отображением следующей реплики
        foreach (char letter in dialogues[currentDialogueIndex])
        {
            dialogueText.text += letter; // Добавить одну букву к тексту
            yield return new WaitForSeconds(typingSpeed); // Подождите немного перед отображением следующего символа
        }
        isTyping = false;
    }

    void LoadDialoguesFromFile(string filePath)
    {
        // Проверяем, существует ли файл
        if (File.Exists(filePath))
        {
            // Читаем все строки из файла
            string[] lines = File.ReadAllLines(filePath);
            // Создаем массив для реплик
            dialogues = new string[lines.Length];
            // Копируем строки в массив реплик
            for (int i = 0; i < lines.Length; i++)
            {
                dialogues[i] = lines[i];
            }
        }
        else
        {
            Debug.LogError("Файл с репликами не найден: " + filePath);
        }
    }

    void EndDialogue()
    {
        // Ваша логика завершения диалога
        Debug.Log("Диалог завершен.");
    }
}
