using System.Collections;
using UnityEngine;

public class CatSleep : MonoBehaviour {
    
    private Coroutine delayEndCoroutine = null;
    private Animator animator;


    private const float interval = 1.3f;
    private const float sleepDuration = 15f;
    
    private bool isSleeping = false;
    private void Awake() {
        animator = GetComponent<Animator>();
        GameEvents.onStateSwitched.AddListener((state) => OnStateSwitched(state));
        GameEvents.onTransitOutComplete.AddListener((shownAd) => QuitSleep());
    }

    private void OnStateSwitched(StateHandler.State state) {
        if (state == StateHandler.State.SLEEP) {
            StartSleep();
        } else if (state == StateHandler.State.DEFAULT && isSleeping) {
            QuitSleep();
        }
    }

    private void StartSleep() {
        Debug.Log("Sleep: Start");
        StartCoroutine(Sleep());
    }

    public void StartSleepLoop() {
        Debug.Log("Sleep: Loop Start");
        StartCoroutine(LoopSleep());
    }

    public void QuitSleep() {
        Debug.Log("Sleep: Quit");

        isSleeping = false;
        
        if (delayEndCoroutine is not null) {
            StopCoroutine(delayEndCoroutine);
        }
    }

    private IEnumerator DelaySleepEnd() {
        yield return new WaitForSeconds(sleepDuration);
        QuitSleep();
    }

    private IEnumerator LoopSleep() {
        isSleeping = true;
        while(isSleeping) {
            GameEvents.onSETime.Invoke("sleep");
            animator.Play("Base Layer.SleepUp");
            
            yield return new WaitForSeconds(interval);
            if (!isSleeping) break;
            
            GameEvents.onSETime.Invoke("sleep");
            animator.Play("Base Layer.SleepDown");
        }
    }

    private IEnumerator Sleep() {
        delayEndCoroutine = StartCoroutine(DelaySleepEnd());

        yield return LoopSleep();

        Debug.Log("Sleep: End");
        GameEvents.onActionEnd.Invoke();
    }
}
