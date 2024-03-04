using System.Collections;
using UnityEngine;

public class Talk : MonoBehaviour {
    [SerializeField] private float showInterval = 0.5f;
    private Fukidashi fukidashi;
    private ScenarioData scenarioData;

    private void Awake() {
        fukidashi = GetComponent<Fukidashi>();
        scenarioData = Resources.Load<ScenarioData>("ScenarioData");

        GameEvents.onStateSwitched.AddListener((state) => OnStateSwitched(state));
    }

    private void OnStateSwitched(StateHandler.State state) {
        if (state == StateHandler.State.PRE_EPILOGUE) return;

        switch (state) {
            case StateHandler.State.TALK: RandomTalk(); break;
            case StateHandler.State.GORON: GoronTalk(); break;
            default: HideFukidashi(); break;
        }
    }

    public IEnumerator StoryTalk(string storyType) {
        
        DayScenario dayScenario = scenarioData.GetDayScenario(storyType);
        string[] lines = dayScenario.Contents;
        
        foreach (string line in lines) {    
            yield return new WaitForSeconds(showInterval);
            fukidashi.ShowNormal(line);
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            fukidashi.HideNormal();
        }
        Debug.Log($"{storyType} Day {PlayerData.Day}: End");
    }

    private void RandomTalk() {
        string[] lines = scenarioData.GetDayScenario("Random").Contents;
        int choice = Random.Range(0, lines.Length);
        string line = lines[choice];
        fukidashi.ShowNormal(line);
    }

    private void GoronTalk() {
        fukidashi.ShowGoron();
    }

    private void HideFukidashi() {
        fukidashi.HideNormal();
    }

}
