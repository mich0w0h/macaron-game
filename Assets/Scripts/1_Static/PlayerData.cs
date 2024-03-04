using UnityEngine;

public static class PlayerData {
    private readonly static string _dayKey = "Day";
    private static bool _clearedLastDay = false;

    public static int Day {
        get => PlayerPrefs.GetInt(_dayKey, 1);
        set { PlayerPrefs.SetInt(_dayKey, value); }
    }

    public static void ClearLastDay() {
        _clearedLastDay = true;
    }

    public static bool IsClearedLastDay() {
        return _clearedLastDay;
    }

    public static void RemoveData() {
        PlayerPrefs.DeleteAll();
    }

    public static bool IsFirstDay() {
        return Day == 1;
    }

    public static bool IsLastDay() {
        return Day == 4;
    }

    public static bool IsSecondDay() {
        return Day == 2;
    }

    public static bool IsThirdDay() {
        return Day == 3;
    }
    public static bool IsAlreadyFriend() {
        return Day < 3;
    }

    public static void ResetToNew() {
        Day = 1;
        _clearedLastDay = false;
    }

    public static void AdvanceDay() {
        Day += 1;
    }
    
}
