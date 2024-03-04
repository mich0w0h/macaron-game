using System.Collections;
using UnityEngine;
using TMPro;

public class DayScene : MonoBehaviour {

    private TextMeshProUGUI numberText;
    CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        numberText = GetComponentsInChildren<TextMeshProUGUI>()[0];

        canvasGroup.alpha = 0f;
        numberText.text = PlayerData.Day.ToString();
        
        GameEvents.onTransitInComplete.AddListener((_)=> StartCoroutine(Play()));
    }


    private IEnumerator Play() {
        Debug.Log("Day Scene: " + PlayerData.Day);

        const float waitDuration = 1.5f;
        const float fadeDuration = 1.5f;
        
        yield return LerpAnimation.Fade(canvasGroup, 0f, 1f, fadeDuration);
        yield return new WaitForSeconds(waitDuration);
        
        GameEvents.onScenePlayEnd.Invoke();
    }    
}
