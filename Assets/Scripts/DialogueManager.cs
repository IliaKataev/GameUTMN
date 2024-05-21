using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        LoadDialoguesFromFile(dialogueFilePath);
        StartCoroutine(DisplayNextDialogue());
    }

    void Update()
    {
        // Проверяем, был ли произведен клик левой кнопкой мыши
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            if (currentDialogueIndex < dialogues.Length - 1)
            {
                currentDialogueIndex++;
                StartCoroutine(DisplayNextDialogue());
            }
            else
            {
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
        if (System.IO.File.Exists(filePath))
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            dialogues = lines;
        }
        else
        {
            Debug.LogError("Файл с репликами не найден: " + filePath);
        }
    }

    void EndDialogue()
    {
        SceneManager.LoadScene("levels"); // Переход на следующую сцену
    }
}
