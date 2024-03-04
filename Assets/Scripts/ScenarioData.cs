using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DayScenario {
    public string Key = "";
    public string[] Contents;
}

public class ScenarioData : ScriptableObject {
    [SerializeField]
    private List<DayScenario> dayScenarios = new();

    public void Init(List<DayScenario> dayScenarios) {
        this.dayScenarios = dayScenarios;
    }

    public DayScenario GetDayScenario(string type) {
        int day = PlayerData.Day;
        DayScenario targetDay = dayScenarios.Find(dayScenario => dayScenario.Key == $"{day}_{type}");

        if (targetDay is null) {
            Debug.LogWarning($"The {type} type scenario of the day {day} doesn't exists.");
        }
        return targetDay;
    }
}
