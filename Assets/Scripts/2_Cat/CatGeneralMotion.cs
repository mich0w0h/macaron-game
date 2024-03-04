using System.Linq;
using UnityEngine;

public class CatGeneralMotion : MonoBehaviour {    
    private Animator animator = null;
    
    private void Awake() {
        animator = GetComponent<Animator>();
        GameEvents.onStateSwitched.AddListener((state) => OnStateSwitched(state));

        if (PlayerData.IsLastDay()) {
            transform.Find("Body").gameObject.SetActive(false);
            transform.Find("Signboard").gameObject.SetActive(true);
        } else {
            transform.Find("Body").gameObject.SetActive(true);
            transform.Find("Signboard").gameObject.SetActive(false);
        }

        Application.targetFrameRate = 60;
    }

    private void OnStateSwitched(StateHandler.State state) {
        StateHandler.State[] ignoreStates = {
            StateHandler.State.SLEEP,
            StateHandler.State.DREAM,
        };

        if (PlayerData.IsLastDay() || ignoreStates.Contains(state)) return;
        
        animator.Play("Base Layer.Default");
        switch (state) {
            case StateHandler.State.NICO: animator.Play("Base Layer.Nico"); break;    
            case StateHandler.State.GORON: animator.Play("Base Layer.Goron"); break;
        }
    }

    // Animation Event
    private void OnMotionEnd() {
        GameEvents.onActionEnd.Invoke();
    }
    
}
