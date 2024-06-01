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
        "������, ����!",
        "�� ������� ����� ���� �� ��-�� ���������� ��������?",
        "���� � ���� ��� �����, �� �� ���� ������ � �� ������ ����������� ���� ���� � ����.",
        "������ ������� - ������ - ����� ������ ��������.",
        "���� �� ��� ������, ������ ������� ��� ������� � ��������� � �������.",
        "�����, ��� ������� ����!"
    };

    private int currentDialogueIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        StartCoroutine(DisplayNextDialogue());
    }

    void Update()
    {
        // ���������, ��� �� ���������� ���� ����� ������� ���� ��� ������ ������� ������
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
        dialogueText.text = ""; // �������� ����� ����� ������������ ��������� �������
        foreach (char letter in dialogues[currentDialogueIndex])
        {
            dialogueText.text += letter; // �������� ���� ����� � ������
            yield return new WaitForSeconds(typingSpeed); // ��������� ������� ����� ������������ ���������� �������
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        SceneManager.LoadScene("levels"); // ������� �� ��������� �����
    }
}
