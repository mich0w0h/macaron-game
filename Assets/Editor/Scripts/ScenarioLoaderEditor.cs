#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ScenarioLoaderEditor : EditorWindow {

    [MenuItem("Tools/Scenario Data")]
    public static void ShowWindow() {
        GetWindow(typeof(ScenarioLoaderEditor), false, "Create Scenario Data Asset");
    }

    private async void OnGUI() {
        
        string savePath = "Assets/Resources/";
        GUILayout.Label($"Create Scenario Data Asset In:\n'{savePath}'");

        if (GUILayout.Button("Build Scenario Data")) {
            ScenarioLoader scenarioLoader = new();
            ScenarioData scenarioData = await scenarioLoader.CreateScenarioData();
            AssetDatabase.CreateAsset(scenarioData, savePath + "ScenarioData.asset");
            AssetDatabase.SaveAssets();
            
            Debug.Log("Scenario Data has successfully created.");
        }
    }
}
#endif
