using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class StateHandler : MonoBehaviour {
    public enum State {
        DEFAULT = 0,
        NICO = 1,
        GORON,
        TALK,
        SWING,
        SLEEP,
        DREAM,
        PROLOGUE,
        EPILOGUE,
        PRE_PROLOGUE,
        PRE_EPILOGUE,
    }
    private State current = State.PRE_PROLOGUE;
    private Coroutine waitCoroutine = null;
    private bool waitingEpilogue = false;

    private void Awake() {
        GameEvents.onClick.AddListener((objName) => {
            if (objName == "BodyArea") { OnBodyClicked(); }
        });
        GameEvents.onActionEnd.AddListener(OnActionEnd);
        GameEvents.onPointAccumulated.AddListener(() => SetEpilogue());
    }

    private void OnBodyClicked() {
        State[] ignoreStates = {
            State.GORON,
            State.DREAM,
            State.PROLOGUE,
            State.EPILOGUE,
            State.PRE_PROLOGUE,
            State.PRE_EPILOGUE,
        };

        if (ignoreStates.Contains(current)) {
            return;
        }

        if (waitingEpilogue) {
            StartCoroutine(PreEpilogue());
            return;
        }

        if (current == State.SWING) {
            SwitchState(State.DEFAULT);
        }

        SetNext();
    }

    private IEnumerator PreEpilogue () {
        if (current == State.PRE_EPILOGUE) yield break;

        SwitchState(State.PRE_EPILOGUE);
        yield return new WaitForSeconds(1f); 
        
        if (waitCoroutine is not null) {
            StopCoroutine(waitCoroutine);
        }
        GameEvents.onSETime.Invoke("accumulated");
        
        yield return new WaitForSeconds(2f);    // Show last action during this time

        waitingEpilogue = false;
        SwitchState(State.EPILOGUE);        
    }

    private void SetEpilogue() {
        State[] waitingStates = {
            State.GORON,
            State.DREAM,
            State.SWING,
        };

        if (waitingStates.Contains(current)) {
            waitingEpilogue = true;
        } else {
            StartCoroutine(PreEpilogue());
        }
    }

    private void OnActionEnd() {
        if (waitingEpilogue) {
            StartCoroutine(PreEpilogue());
            return;
        }

        if (current == State.PRE_PROLOGUE) {    // swing end before prologue
            SwitchState(State.PROLOGUE);
            return;
        }

        SwitchState(State.DEFAULT);
        SetWaitState();
    }
 
    private void SetNext() {
        State next = ChooseNext();

        SwitchState(next);
        SetWaitState();
    }

    private State ChooseNext() {
        if (current != State.DEFAULT) {
            return State.DEFAULT;
        }

        if (PlayerData.IsFirstDay() || PlayerData.IsLastDay()) {
            return State.TALK;
        }

        int choice = UnityEngine.Random.Range(1, 7);
        if (choice >= (int)State.TALK) {
            return State.TALK;
        } else {
            return (State)Enum.ToObject(typeof(State), choice);
        }
    }

    private void SetWaitState() {
        if (PlayerData.IsLastDay()) return;

        if (waitCoroutine is not null) {
            StopCoroutine(waitCoroutine);
        }

        State waitState;
        if (PlayerData.IsFirstDay()) {
            waitState = State.SWING;    
        } else {
            int choice = UnityEngine.Random.Range(0, 2);
            waitState = (choice == 0) ? State.SLEEP : State.SWING;
        }
        const float waitDuration = 6f;
        waitCoroutine = StartCoroutine(WaitSwitchState(waitState, waitDuration));
    }
    

    private void SwitchState(State state) {
        current = state;
        Debug.Log($"State Switched: {current}");
        GameEvents.onStateSwitched.Invoke(current);
    }

    private IEnumerator WaitSwitchState(State waitState, float waitDuration) {
        Debug.Log($"State Wait: {waitState}");
        yield return new WaitForSeconds(waitDuration);
        SwitchState(waitState);

        if (waitState == State.SLEEP) {
            StopCoroutine(waitCoroutine);
            waitCoroutine = StartCoroutine(WaitSwitchState(State.DREAM, 4.5f));
        }
    }

}