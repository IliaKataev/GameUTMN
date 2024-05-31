using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakeButtonEffect : MonoBehaviour
{
    public Button shakeButton;
    public Color highlightColor = Color.yellow;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 10f;
    public float highlightDuration = 0.5f;

    private Vector3 originalPosition;
    private Color originalColor;

    void Start()
    {
        originalPosition = shakeButton.transform.localPosition;
        originalColor = shakeButton.image.color;
    }

    public void TriggerShakeAndHighlight()
    {
        StartCoroutine(Shake());
        StartCoroutine(Highlight());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            shakeButton.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        shakeButton.transform.localPosition = originalPosition;
    }

    private IEnumerator Highlight()
    {
        shakeButton.image.color = highlightColor;
        yield return new WaitForSeconds(highlightDuration);
        shakeButton.image.color = originalColor;
    }
}
