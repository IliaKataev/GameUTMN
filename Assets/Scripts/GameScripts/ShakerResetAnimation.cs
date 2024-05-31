using UnityEngine;
using System.Collections;

public class ShakerResetAnimation : MonoBehaviour
{
    public Transform shakerTransform;
    public float animationDuration = 2f;
    public float moveDistance = 1f;
    public float firstRotationAngle = 45f;
    public float secondRotationAngle = 90f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isAnimating = false;

    void Start()
    {
        //сохраняем исходные значения позиции и вращения шейкера
        originalPosition = shakerTransform.localPosition;
        originalRotation = shakerTransform.localRotation;
    }

    public void StartShakerAnimation()
    {
        if (!isAnimating)
        {
            StartCoroutine(AnimateShaker());
        }
    }

    private IEnumerator AnimateShaker()
    {
        isAnimating = true;

        float halfDuration = animationDuration / 3f;
        float quarterDuration = animationDuration / 5f;

        //Приподнимаем шейкер
        Vector3 raisedPosition = originalPosition + Vector3.up * 0.1f;
        yield return MoveToPosition(shakerTransform, raisedPosition, quarterDuration);

        //Наклоняем на 45 градусов
        Quaternion firstTiltRotation = originalRotation * Quaternion.Euler(0, 0, firstRotationAngle);
        yield return RotateToRotation(shakerTransform, firstTiltRotation, quarterDuration);

        //Еще двигаем влево
        Vector3 rightPosition = raisedPosition + Vector3.right * moveDistance;
        yield return MoveToPosition(shakerTransform, rightPosition, quarterDuration);

        // Step 4:Наклоняем на 90 градусов
        Quaternion secondTiltRotation = firstTiltRotation * Quaternion.Euler(0, 0, secondRotationAngle);
        yield return RotateToRotation(shakerTransform, secondTiltRotation, quarterDuration);

        //Обратная анимация
        yield return RotateToRotation(shakerTransform, firstTiltRotation, quarterDuration);
        yield return MoveToPosition(shakerTransform, raisedPosition, quarterDuration);
        yield return RotateToRotation(shakerTransform, originalRotation, quarterDuration);
        yield return MoveToPosition(shakerTransform, originalPosition, quarterDuration);

        isAnimating = false;
    }

    private IEnumerator MoveToPosition(Transform transform, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }

    private IEnumerator RotateToRotation(Transform transform, Quaternion targetRotation, float duration)
    {
        Quaternion startRotation = transform.localRotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;
    }
}
