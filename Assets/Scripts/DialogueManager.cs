using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.1f;

    private string[] dialogues = new string[]
    {
        "Привет, друг!",
        "Ты случаем зашел сюда не из-за чемпионата барменов?",
        "Если у тебя нет опыта, то мы тебя научим и ты можешь попробовать свои силы в деле.",
        "Выбери уровень - Стажер - чтобы пройти обучение.",
        "Если ты все знаешь, выбери новичок или эксперт и приступай к заказам.",
        "Удачи, мой дорогой друг!"
    };

    private int currentDialogueIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        StartCoroutine(DisplayNextDialogue());
    }

    void Update()
    {
        // Проверяем, был ли произведен клик левой кнопкой мыши или нажата клавиша пробел
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isTyping)
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

    void EndDialogue()
    {
        SceneManager.LoadScene("levels"); // Переход на следующую сцену
    }
}
