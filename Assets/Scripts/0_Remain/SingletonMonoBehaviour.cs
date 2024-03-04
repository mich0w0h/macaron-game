using UnityEngine;

public class SingletonMonoBehaviour : MonoBehaviour {
    private bool startCalled = false;
    
    // need to be called after Awake() of RemainingGroup script
    protected virtual void Start() {
        if (!startCalled) {
            startCalled = true;
            OnStart();
            Debug.Log("Singleton Start: " + name);
        }
    }

    protected virtual void OnStart() {}
}
