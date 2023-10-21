using UnityEditor;
using UnityEngine;

public class MainEditorSetting : ScriptableObject {
    public string TitleSign;
    public int DisplayWeight;

    public virtual void OnGUI() {
    }

    public MainEditorMainSetting GetMainSetting() {
        return AssetDatabase.LoadAssetAtPath<MainEditorMainSetting>(
            "Assets/Scripts/Editor/MainEditor/Config/MainEditorMainSetting.asset");
    }
}