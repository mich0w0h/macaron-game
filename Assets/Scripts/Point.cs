using System.Collections;
using UnityEngine;
using TMPro;

public class Point : MonoBehaviour {
    private Animator upAnimator;
    private TextMeshProUGUI countText;
    
    private readonly float upInterval = 0.08f;
    private int maxPoint = 0;
    private int currentPoint = 0;
    private int shownPoint = 0;

    private ScenarioData scenarioData = null;

    private void Awake() {
        upAnimator = GetComponentInChildren<Animator>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
        scenarioData = Resources.Load<ScenarioData>("ScenarioData");

        GameEvents.onStateSwitched.AddListener((state) => OnStateSwitched(state));

        maxPoint = int.Parse(scenarioData.GetDayScenario("Point").Contents[0]);
        UpdateText();
    }

    private void OnStateSwitched(StateHandler.State state) {
        switch (state) {
            case StateHandler.State.NICO: AwardNico(); break;
            case StateHandler.State.TALK: AwardTalk(); break;
            case StateHandler.State.GORON: AwardGoron(); break;
            case StateHandler.State.DREAM: AwardDream(); break;
        }
    }

    private void AwardTalk() {
        const int awardPoint = 1;
        StartCoroutine(UpSequence(awardPoint));
    }

    private void AwardGoron() {
        const int awardPoint = 2;
        StartCoroutine(UpSequence(awardPoint));
    }

    private void AwardNico() {
        const int awardPoint = 2;
        StartCoroutine(UpSequence(awardPoint));
    }

    private void AwardDream() {
        const int awardPoint = 20;
        StartCoroutine(UpSequence(awardPoint));
    }

    private void IncrementText() {
        GameEvents.onSETime.Invoke("pointup");
        shownPoint += 1;
        UpdateText();
    }

    private void UpdateText() {
        countText.text = $"{shownPoint}/{maxPoint}";
    }

    private bool ReachedMax() {
        return currentPoint >= maxPoint;
    }

    private bool AccumulatedMax() {
        return currentPoint >= maxPoint;
    }

    private IEnumerator UpSequence(int num) {
        for (int i=0; i<num; i++) {
            if (ReachedMax()) {
                num = i;
                break;
            }
            currentPoint++;
        }

        if (AccumulatedMax()) {
            GameEvents.onPointAccumulated.Invoke();
        }
        
        for (int i=0; i<num; i++) {
            upAnimator.SetTrigger("ToUp");
            yield return new WaitForSeconds(upInterval);

            IncrementText();
            yield return new WaitForSeconds(upInterval);
            
            upAnimator.SetTrigger("ToDefault");
            yield return new WaitForSeconds(upInterval);
        }
    }


}
