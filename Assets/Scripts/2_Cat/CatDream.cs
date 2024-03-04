using UnityEngine;

public class CatDream : MonoBehaviour {
    private CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();        
        GameEvents.onStateSwitched.AddListener(OnStateSwitched);
        GameEvents.onActionEnd.AddListener(OnActionEnd);
        
        Hide();
    }

    private void OnStateSwitched(StateHandler.State state) {
        if (state == StateHandler.State.DREAM) {
            Show();
        }
    }

    private void OnActionEnd() {
        Hide();
    }

    private void Show() {
        GameEvents.onSETime.Invoke("dream");
        StartCoroutine(LerpAnimation.Fade(canvasGroup, 0f, 1f, 2.5f));
    }

    private void Hide() {
        canvasGroup.alpha = 0f;
    }
}
