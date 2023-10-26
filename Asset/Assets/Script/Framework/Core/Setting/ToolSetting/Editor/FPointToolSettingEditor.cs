using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FPointToolSetting))]
public class FPointToolSettingEditor : Editor {
    public GameObject pointToolProbe;

    public override void OnInspectorGUI() {
        FPointToolSetting setting = (FPointToolSetting)target;

        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button($"返回", GUILayout.Height(30))) {
            FEditorCommon.JumpToTarget(false, setting.FromSetting);
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Label($"游戏管理器 - 地图编辑器 - {setting.FromSettingName} - 点位配置");

        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("加载 点位工具", GUILayout.Height(30))) {
            pointToolProbe = new GameObject("点位读取工具");
            pointToolProbe.transform.localPosition = Vector3.zero;
            pointToolProbe.transform.localRotation = Quaternion.identity;
            FGetPointTool tool = pointToolProbe.AddComponent<FGetPointTool>();
            tool.FpointToolSetting = setting;
            tool.SettingName = setting.FromSettingName + " - 点位读取工具";

            for (int i = 0; i < setting.FPointDatas.Count; i++) {
                FPointToolSetting.FPointData fPointData = setting.FPointDatas[i];
                GameObject child = Instantiate(setting.FPointPrefab);
                child.name = "原始点位_" + i;
                child.transform.position = fPointData.FPointPos;
                child.transform.rotation = fPointData.FPointRot;
                child.transform.SetParent(pointToolProbe.transform);
            }

            FEditorCommon.JumpToTarget(false, pointToolProbe);
        }

        if(GUILayout.Button("创建 点位工具", GUILayout.Height(30))) {
            pointToolProbe = new GameObject("点位读取工具");
            pointToolProbe.transform.localPosition = Vector3.zero;
            pointToolProbe.transform.localRotation = Quaternion.identity;
            FGetPointTool tool = pointToolProbe.AddComponent<FGetPointTool>();
            tool.FpointToolSetting = setting;
            FEditorCommon.JumpToTarget(false, pointToolProbe);
        }
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}