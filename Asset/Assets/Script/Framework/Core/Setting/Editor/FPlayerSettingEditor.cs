using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FPlayerSetting))]
public class FPlayerSettingEditor : Editor {
    public override void OnInspectorGUI() {
        FPlayerSetting setting = (FPlayerSetting)target;
        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("返回", GUILayout.Height(30))) {
            FEditorCommon.JumpToTarget(false, FindObjectOfType<FMapEditorTool>());
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("游戏管理器 - 地图编辑器 - " + setting.SettingName);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("打开 点位工具", GUILayout.Height(30))) {
            FEditorCommon.JumpToTarget(false, setting.FPointToolSetting);
        }

        if (GUILayout.Button("生成 新点位配置", GUILayout.Height(30))) {
            setting.FPointToolSetting = CreateInstance<FPointToolSetting>();

            string path = "Assets/Script/Framework/Setting/ToolSetting/FPointToolSetting_Player.asset";
            FPointToolSetting scriptableObject = CreateInstance<FPointToolSetting>();
            AssetDatabase.CreateAsset(scriptableObject, path);
            AssetDatabase.SaveAssets();

            setting.FPointToolSetting = AssetDatabase.LoadAssetAtPath<FPointToolSetting>(path);
            setting.FPointToolSetting.FromSetting = setting;
            setting.FPointToolSetting.FromSettingName = setting.SettingName;

            FEditorCommon.JumpToTarget(false, setting.FPointToolSetting);
        }
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}