using System.Collections;
using UnityEngine;
using TMPro;

public class EndingScene : MonoBehaviour {

    private TextMeshProUGUI greetingText;
    private CanvasGroup credit1;
    private TextMeshProUGUI credit2;

    private void Awake() {
        greetingText = transform.Find("GreetingText").GetComponent<TextMeshProUGUI>();
        credit2 = transform.Find("Credit2").GetComponent<TextMeshProUGUI>();
        credit1 = GetComponentInChildren<CanvasGroup>();

        TransparentCredit(credit1);
        TransparentCredit(credit2);
        TransparentCredit(greetingText);

        GameEvents.onTransitInComplete.AddListener((_)=> StartCoroutine(Play()));
    }

    private IEnumerator Play() {
        Debug.Log("Game Clear");

        const float waitDuration = 1.5f;
        const float fadeDuration = 2f;
        
        yield return new WaitForSeconds(0.5f);

        yield return ShowHideCredit(greetingText, waitDuration, fadeDuration);
        yield return new WaitForSeconds(waitDuration);
        
        yield return ShowHideCredit(credit1, waitDuration, fadeDuration);
        yield return new WaitForSeconds(waitDuration);

        yield return ShowHideCredit(credit2, waitDuration, fadeDuration);
        yield return new WaitForSeconds(waitDuration);
        
        PlayerData.ResetToNew();
        GameEvents.onScenePlayEnd.Invoke();
    }

    private void TransparentCredit(TextMeshProUGUI textMeshPro) {
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);
    }

    private void TransparentCredit(CanvasGroup canvasGroup) {
        canvasGroup.alpha = 0f;
    }

    private IEnumerator ShowHideCredit(TextMeshProUGUI textMeshPro, float waitDuration, float fadeDuration) {
        yield return LerpAnimation.Fade(textMeshPro, 0f, 1f, fadeDuration);
        yield return new WaitForSeconds(waitDuration);
        yield return LerpAnimation.Fade(textMeshPro, 1f, 0f, fadeDuration);        
    }

    private IEnumerator ShowHideCredit(CanvasGroup canvasGroup, float waitDuration, float fadeDuration) {
        yield return LerpAnimation.Fade(canvasGroup, 0f, 1f, fadeDuration);
        yield return new WaitForSeconds(waitDuration);
        yield return LerpAnimation.Fade(canvasGroup, 1f, 0f, fadeDuration);        
    }
}
