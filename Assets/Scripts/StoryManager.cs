using System.Collections;
using UnityEngine;

public class StoryManager : MonoBehaviour {
    private Talk talk;
    private CatSwing swing;
    private CatSleep sleep;
    const float zoomScale = 1.2f;
    const float zoomDuration = 1f;
    private void Awake() {
        talk = GetComponentInChildren<Talk>();
        sleep = GetComponentInChildren<CatSleep>();
        swing = GetComponentInChildren<CatSwing>();

        GameEvents.onStateSwitched.AddListener((state) => OnStateSwitched(state));
        GameEvents.onTransitInComplete.AddListener((_) => StartCoroutine(PrePrologue()));
    }

    private void OnStateSwitched(StateHandler.State state) {
        switch (state) {
            case StateHandler.State.PROLOGUE:
                StartCoroutine(Prologue());
                break;
            case StateHandler.State.EPILOGUE:
                StartCoroutine(Epilogue());
                break;
        }
    }

    private IEnumerator PrePrologue() {
        yield return new WaitForSeconds(1f);
        yield return LerpAnimation.Zoom(transform, 1f, zoomScale, zoomDuration);
        
        if (PlayerData.IsLastDay()) {
            // need to invoke because swing wouldn't start
            GameEvents.onActionEnd.Invoke();
        } else {
            swing.QuitSwing();
        }
    }

    private IEnumerator Prologue() {
        yield return new WaitForSeconds(1f);
        yield return talk.StoryTalk("Prologue");
        yield return new WaitForSeconds(1f);

        yield return LerpAnimation.Zoom(transform, zoomScale, 1f, zoomDuration);

        GameEvents.onActionEnd.Invoke();
    }

    private IEnumerator Epilogue() {
        yield return new WaitForSeconds(1f);
        yield return LerpAnimation.Zoom(transform, 1f, zoomScale, zoomDuration);
        
        yield return new WaitForSeconds(1f);
        
        if(PlayerData.IsLastDay()) GameEvents.onLastEpilogueStart.Invoke();
        yield return talk.StoryTalk("Epilogue");
        
        yield return new WaitForSeconds(1f);

        yield return AfterEpilogue();
    }

    private IEnumerator AfterEpilogue() {
        if (PlayerData.IsThirdDay()) {
            sleep.StartSleepLoop();
            yield return new WaitForSeconds(4.5f);
        }
        
        if (PlayerData.IsLastDay()) {
            PlayerData.ClearLastDay();
        } else {
            PlayerData.AdvanceDay();
        }

        GameEvents.onScenePlayEnd.Invoke();

    }
}
