using System.Collections;
using UnityEngine;

public class CatSwing : MonoBehaviour {
    private Animator animator = null;


    private bool inSwing = false;
    private void Awake() {
        animator = GetComponent<Animator>();
        GameEvents.onStateSwitched.AddListener((state) => OnStateSwitched(state));
    }

    private void Start() {
        // Swing before Prologue
        if (!PlayerData.IsLastDay()) {
            StartSwing();
        }
    }
    

    private void OnStateSwitched(StateHandler.State state) {
        if (state == StateHandler.State.SWING) {
            StartSwing();
        } else if (state == StateHandler.State.DEFAULT && inSwing) {
            QuitSwing();
        }
    }

    public void QuitSwing() {
        if (!inSwing) return;
        
        Debug.Log("Swing: Quit");

        animator.SetBool("isSwing", false);
        inSwing = false;
    }

    public void StartSwing() {
        StartCoroutine(Swing());
    }

    private IEnumerator Swing() {
        Debug.Log("Swing: Start");

        animator.SetBool("isSwing", true);
        inSwing = true;
        
        yield return new WaitForSeconds(7f);
        
        animator.SetBool("isSwing", false);
        inSwing = false;
    }
}
