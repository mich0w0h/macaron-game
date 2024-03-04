using UnityEngine.Events;

public static class GameEvents {
    public static UnityEvent onActionEnd = new();
    public static UnityEvent onPointAccumulated = new();
    public static UnityEvent<string> onTransitInComplete = new();
    public static UnityEvent<bool> onTransitOutComplete = new();
    public static UnityEvent onScenePlayEnd = new();
    public static UnityEvent onLastEpilogueStart = new();
    public static UnityEvent onAdShown = new();
    public static UnityEvent onAdComplete = new();
    public static UnityEvent<StateHandler.State> onStateSwitched = new();
    public static UnityEvent<string> onSETime = new();
    public static UnityEvent<string> onClick = new();
}
