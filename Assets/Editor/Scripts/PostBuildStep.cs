#if UNITY_EDITOR && UNITY_IOS
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public class PostBuildStep
{
    private const string k_TrackingDescription = "アクティビティの追跡（トラッキング）を許可をすることで、よりあなたに合った広告が表示されやすくなります。";

    [PostProcessBuild(0)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            AddPListValues(pathToXcode);
            ModifyFrameworks(pathToXcode);
        }
    }

    static void AddPListValues(string pathToXcode)
    {
        string plistPath = pathToXcode + "/Info.plist";
        PlistDocument plistObj = new PlistDocument();
        plistObj.ReadFromString(File.ReadAllText(plistPath));
        PlistElementDict plistRoot = plistObj.root;
        plistRoot.SetString("NSUserTrackingUsageDescription", k_TrackingDescription);
        File.WriteAllText(plistPath, plistObj.WriteToString());
    }

    private static void ModifyFrameworks(string path)
    {
        string projPath = PBXProject.GetPBXProjectPath(path);
        
        var project = new PBXProject();
        project.ReadFromFile(projPath);

        string mainTargetGuid = project.GetUnityMainTargetGuid();
        
        foreach (var targetGuid in new[] { mainTargetGuid, project.GetUnityFrameworkTargetGuid() })
        {
            project.SetBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
        }
        
        project.SetBuildProperty(mainTargetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");

        project.WriteToFile(projPath);
    }
}

#endif
