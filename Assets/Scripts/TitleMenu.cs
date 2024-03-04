using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {
    private RectTransform startButton;
    private RectTransform continueButton;

    private void Awake() {
        Button[] buttons = GetComponentsInChildren<Button>();
        startButton = buttons[0].GetComponent<RectTransform>();
        continueButton = buttons[1].GetComponent<RectTransform>();
        
        GameEvents.onClick.AddListener(HandleClick);
        
        if (PlayerData.IsFirstDay()) {
            startButton.anchoredPosition = new Vector2(0, 0);
            continueButton.gameObject.SetActive(false);
        } else {
            startButton.anchoredPosition = new Vector2(0, -160);
            continueButton.anchoredPosition = new Vector2(0, 0);
        }
    }

    private void HandleClick(string objName) {
        switch (objName) {
            case "StartButton": NewGame(); break;
            case "ContinueButton": LoadGame(); break;
            case "LinkToPolicy": OpenPrivacyPolicy(); break;
        }
    }

    private void OpenPrivacyPolicy() {
        Application.OpenURL("https://gist.github.com/mich0w0h/8c6e333a00701e3bc4a129ed8e46ce46");
    }
    

    private void NewGame() {
        PlayerData.ResetToNew();
        LoadGame();
    }

    private void LoadGame() {
        GameEvents.onSETime.Invoke("gamestart");
        GameEvents.onScenePlayEnd.Invoke();
    }
}
