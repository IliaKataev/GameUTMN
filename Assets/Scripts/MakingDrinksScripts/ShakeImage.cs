using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShakeImageOnButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float shakeDuration = 0.5f; // Продолжительность тряски
    public float shakeMagnitude = 0.1f; // Сила тряски
    public GameObject targetImage; // Объект картинки, который будет трястись

    [SerializeField]
    public static bool isShakeButtonPressed = false;

    private Vector3 originalPosition; // Начальная позиция картинки
    private bool isShaking = false; // Флаг, указывающий, трясется ли картинка

    void Start()
    {
        // Сохраняем начальную позицию картинки
        originalPosition = targetImage.transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isShaking)
        {
            // Запускаем тряску картинки
            StartCoroutine(Shake());
            isShakeButtonPressed = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Останавливаем тряску, если она запущена
        StopAllCoroutines();
        // Возвращаем картинку в исходную позицию
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

        // Возвращаем картинку в исходную позицию
        targetImage.transform.position = originalPosition;
        isShaking = false;
    }
}