using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShakeImageOnButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float shakeDuration = 0.5f; // ����������������� ������
    public float shakeMagnitude = 0.1f; // ���� ������
    public GameObject targetImage; // ������ ��������, ������� ����� ��������

    [SerializeField]
    public static bool isShakeButtonPressed = false;

    private Vector3 originalPosition; // ��������� ������� ��������
    private bool isShaking = false; // ����, �����������, �������� �� ��������

    void Start()
    {
        // ��������� ��������� ������� ��������
        originalPosition = targetImage.transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isShaking)
        {
            // ��������� ������ ��������
            StartCoroutine(Shake());
            isShakeButtonPressed = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ������������� ������, ���� ��� ��������
        StopAllCoroutines();
        // ���������� �������� � �������� �������
        targetImage.transform.position = originalPosition;
        isShaking = false;
    }

    IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            targetImage.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // ���������� �������� � �������� �������
        targetImage.transform.position = originalPosition;
        isShaking = false;
    }
}