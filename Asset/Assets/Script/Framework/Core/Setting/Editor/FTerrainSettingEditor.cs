using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FTerrainSetting))]
public class FTerrainSettingEditor : Editor {
    public override void OnInspectorGUI() {
        FTerrainSetting fTerrainSetting = (FTerrainSetting)target;
        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("返回", GUILayout.Height(30))) {
            FEditorCommon.JumpToTarget(false, FindObjectOfType<FMapEditorTool>());
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("游戏管理器 - 地图编辑器 - " + fTerrainSetting.SettingName);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("打开 点位工具", GUILayout.Height(30))) {
            FEditorCommon.JumpToTarget(false, fTerrainSetting.FPointToolSetting);
        }

        if (GUILayout.Button("生成 新点位配置", GUILayout.Height(30))) {
            fTerrainSetting.FPointToolSetting = CreateInstance<FPointToolSetting>();

            string path = "Assets/Script/Framework/Setting/ToolSetting/FPointToolSetting_Terrain.asset";
            FPointToolSetting scriptableObject = CreateInstance<FPointToolSetting>();
            AssetDatabase.CreateAsset(scriptableObject, path);
            AssetDatabase.SaveAssets();

            fTerrainSetting.FPointToolSetting = AssetDatabase.LoadAssetAtPath<FPointToolSetting>(path);
            fTerrainSetting.FPointToolSetting.FromSetting = fTerrainSetting;
            fTerrainSetting.FPointToolSetting.FromSettingName = fTerrainSetting.SettingName;

            FEditorCommon.JumpToTarget(false, fTerrainSetting.FPointToolSetting);
        }

        EditorGUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}