using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FGetPointTool))]
public class FGetPointToolEditor : Editor {
    public override void OnInspectorGUI() {
        FGetPointTool fGetPointTool = (FGetPointTool) target;
        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("销毁", GUILayout.Height(30))) {
            DestroyImmediate(fGetPointTool.gameObject);
            FEditorCommon.JumpToTarget(false, fGetPointTool.FpointToolSetting);
            return;
        }
        EditorGUILayout.EndHorizontal();
        
        GUILayout.Label("游戏管理器 - 地图编辑器 - " + fGetPointTool.SettingName);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("创建子点位", GUILayout.Height(30))) {
            GameObject childPoint = Instantiate(fGetPointTool.FpointToolSetting.FPointPrefab);
            childPoint.name = "未保存点位";
            childPoint.transform.SetParent(fGetPointTool.transform);
            childPoint.transform.localPosition = Vector3.zero;
            childPoint.transform.localRotation = Quaternion.identity;

            FEditorCommon.JumpToTarget(true, childPoint);
        }

        if (GUILayout.Button("更新子点位", GUILayout.Height(30))) {
            fGetPointTool.FpointToolSetting.FPointDatas.Clear();
            foreach (Transform child in fGetPointTool.transform) {
                fGetPointTool.FpointToolSetting.FPointDatas.Add(new FPointToolSetting.FPointData() {
                    FPointPos = child.position,
                    FPointRot = child.rotation
                });
                child.name = "已保存点位";
            }
            AssetDatabase.SaveAssetIfDirty(fGetPointTool.FpointToolSetting);
            AssetDatabase.Refresh();
        }

        EditorGUILayout.EndHorizontal();
        
        base.OnInspectorGUI();
    }
}