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
        // ���������, ��� �� ���������� ���� ����� ������� ����
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
        dialogueText.text = ""; // �������� ����� ����� ������������ ��������� �������
        foreach (char letter in dialogues[currentDialogueIndex])
        {
            dialogueText.text += letter; // �������� ���� ����� � ������
            yield return new WaitForSeconds(typingSpeed); // ��������� ������� ����� ������������ ���������� �������
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
            Debug.LogError("���� � ��������� �� ������: " + filePath);
        }
    }

    void EndDialogue()
    {
        SceneManager.LoadScene("levels"); // ������� �� ��������� �����
    }
}
