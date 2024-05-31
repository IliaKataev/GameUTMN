using UnityEngine;
using System.Collections;

public class ShakePanelEffect : MonoBehaviour
{
    public float shakeDuration = 0.5f; // ������������ ������
    public float shakeMagnitude = 10f; // ������������� ������

    private Vector3 originalPosition; // �������� ������� ������

    void Start()
    {
        // ��������� �������� ������� ������
        originalPosition = transform.localPosition;
    }

    public void TriggerShake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // ���������� ������ � �������� ���������
        transform.localPosition = originalPosition;
    }
}
