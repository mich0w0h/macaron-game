using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour {
    [SerializeField] private bool onetimeButton = false;
    private Button button = null;
    string objName = "";

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        DisableClick();

        GameEvents.onTransitInComplete.AddListener((_) => EnableClick());
        
        objName = gameObject.name;
    }

    private void EnableClick() {
        button.enabled = true;
    }

    private void DisableClick() {
        button.enabled = false;
    }

    private bool IsEnabled() {
        return button.enabled;
    }

    private void OnClick() {
        if(!IsEnabled()) return;
        
        Debug.Log($"Clicked: {objName}");
        GameEvents.onClick.Invoke(objName);
        
        if (onetimeButton) {
            DisableClick();
        }
    }
    
}
