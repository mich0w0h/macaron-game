using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class LerpAnimation {
    public static IEnumerator Fade(Image image, float start, float end, float duration) {
        image.color = new Color(image.color.r, image.color.g, image.color.b, start);

        float elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / duration;
            float alpha = Mathf.Lerp(start, end, normalizedTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }
    }

    public static IEnumerator Fade(TextMeshProUGUI textMeshPro, float start, float end, float duration) {
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, start);

        float elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / duration;
            float alpha = Mathf.Lerp(start, end, normalizedTime);
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, alpha);
            yield return null;
        }
    }

    public static IEnumerator Fade(CanvasGroup canvasGroup, float start, float end, float duration) {
        canvasGroup.alpha = start;

        float elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / duration;
            float alpha = Mathf.Lerp(start, end, normalizedTime);
            canvasGroup.alpha = alpha;
            yield return null;
        }
    }

    public static IEnumerator Fade(AudioSource audioSource, float start, float end, float duration) {
        audioSource.volume = start;

        float elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / duration;
            float volume = Mathf.Lerp(start, end, normalizedTime);
            audioSource.volume = volume;
            yield return null;
        }
    }


    public static IEnumerator Zoom(Transform transform, float startScale, float endScale, float duration) {
        transform.localScale = Vector3.one * startScale;
        float elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / duration;
            float scale = Mathf.Lerp(startScale, endScale, normalizedTime);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
    }
}
