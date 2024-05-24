using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShakeOnButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float shakeDuration = 0.5f; // Продолжительность тряски
    public float shakeMagnitude = 0.1f; // Сила тряски

    private Vector3 originalPosition; // Начальная позиция объекта
    private bool isShaking = false; // Флаг, указывающий, трясется ли объект

    void Start()
    {
        // Сохраняем начальную позицию объекта
        originalPosition = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isShaking)
        {
            // Запускаем тряску объекта
            StartCoroutine(Shake());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Останавливаем тряску, если она запущена
        StopAllCoroutines();
        // Возвращаем объект в исходную позицию
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

        // Возвращаем объект в исходную позицию
        transform.position = originalPosition;
        isShaking = false;
    }
}