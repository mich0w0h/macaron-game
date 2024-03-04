using UnityEngine;

public class RemainingGroup : MonoBehaviour
{
    public static RemainingGroup instance = null;
    
    private void Awake() {
        if (instance is null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

}
