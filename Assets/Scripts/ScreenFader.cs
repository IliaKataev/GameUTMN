using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public float fadeSpeed = 1f;
    public Image fadeImage;

    private bool isFading = false;

    public void StartFade()
    {
        if (!isFading)
        {
            StartCoroutine(FadeScreen());
        }
    }

    IEnumerator FadeScreen()
    {
        isFading = true;
        Color targetColor = new Color(0f, 0f, 0f, 1f); // Черный цвет с полной непрозрачностью
        while (fadeImage.color.a < 1f)
        {
            fadeImage.color = Color.Lerp(fadeImage.color, targetColor, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        isFading = false;
    }
}
