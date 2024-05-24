using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShakeOnButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float shakeDuration = 0.5f; // ����������������� ������
    public float shakeMagnitude = 0.1f; // ���� ������

    private Vector3 originalPosition; // ��������� ������� �������
    private bool isShaking = false; // ����, �����������, �������� �� ������

    void Start()
    {
        // ��������� ��������� ������� �������
        originalPosition = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isShaking)
        {
            // ��������� ������ �������
            StartCoroutine(Shake());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ������������� ������, ���� ��� ��������
        StopAllCoroutines();
        // ���������� ������ � �������� �������
        transform.position = originalPosition;
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

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // ���������� ������ � �������� �������
        transform.position = originalPosition;
        isShaking = false;
    }
}