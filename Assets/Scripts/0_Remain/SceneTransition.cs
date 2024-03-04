using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : SingletonMonoBehaviour {
    public static SceneTransition instance = null;
    [SerializeField] private Image overlay;

    private const float defaultDuration = 2.5f;

    private void Awake() {
        // need to be called at Awake() to fire transition at the game start
        if (instance is null) {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    protected override void Start() {
        base.Start();
    }

    protected override void OnStart() {
        instance = this;

        GameEvents.onAdComplete.AddListener(() => LoadNextScene());
        GameEvents.onScenePlayEnd.AddListener(() => StartCoroutine(TransitOut()));
        
        GameEvents.onTransitOutComplete.AddListener((showAd) => {
            if (!showAd) {
                LoadNextScene();
            }            
        });
    }

    private IEnumerator TransitOut() {
        const float longerDuration = 4f;
        string nextScene = ChooseNextScene();
        
        if (nextScene == "TitleScene" || nextScene == "EndingScene") {
            yield return LerpAnimation.Fade(overlay, 0f, 1f, longerDuration);
        } else {
            yield return LerpAnimation.Fade(overlay, 0f, 1f, defaultDuration);
        }

        string currentScene = GetCurrentSceneName();
        bool showAd = currentScene == "MainScene" && nextScene != "EndingScene";
        Debug.Log("TransitOut: Complete");
        GameEvents.onTransitOutComplete.Invoke(showAd);

    }

    private void LoadNextScene() {
        string nextScene = ChooseNextScene();
        Debug.Log("SceneTransition: Load " + nextScene);
        
        SceneManager.LoadScene(nextScene);
    }

    private string ChooseNextScene() {
        string currentScene =  GetCurrentSceneName();
        string nextScene = "";
        
        switch (currentScene) {
            case "TitleScene": nextScene = "DayScene"; break;
            case "DayScene": nextScene = "MainScene"; break;
            case "EndingScene": nextScene = "TitleScene"; break;
            case "MainScene":
                if (PlayerData.IsClearedLastDay()) {
                    nextScene = "EndingScene";
                } else {
                    nextScene = "DayScene";
                }
                break;
        }

        return nextScene;
    }


    private IEnumerator TransitIn() {
        yield return LerpAnimation.Fade(overlay, 1f, 0f, defaultDuration);

        Debug.Log("TransitIn: Complete");
        string currentScene = GetCurrentSceneName();
        GameEvents.onTransitInComplete.Invoke(currentScene);

    }

    private string GetCurrentSceneName() {
        return SceneManager.GetActiveScene().name;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        StartCoroutine(TransitIn());    
    }

}
