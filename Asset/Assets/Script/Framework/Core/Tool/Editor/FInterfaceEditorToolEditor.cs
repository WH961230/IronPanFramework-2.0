﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FInterfaceEditorTool))]
public class FInterfaceEditorToolEditor : Editor {
    public override void OnInspectorGUI() {
        FInterfaceEditorTool tool = (FInterfaceEditorTool)target;
        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");

        FEditorCommon.LockInspector(true);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("返回", GUILayout.Height(30))) {
            DestroyImmediate(tool.gameObject);
            FEditorCommon.SaveScene();
            FEditorCommon.JumpToTarget(false, tool.fGameManager);
            return;
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("游戏管理器 - 界面编辑器");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("添加界面", GUILayout.Height(30))) {
            
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("编辑界面", GUILayout.Height(30))) {
            tool.isEditorInterface = !tool.isEditorInterface;
        }
        EditorGUILayout.EndHorizontal();

        InterfaceDisplay(tool);
        InterfaceSave(tool);


        base.OnInspectorGUI();
    }

    private void InterfaceDisplay(FInterfaceEditorTool tool) {
        if (tool.isEditorInterface) {
            EditorGUILayout.BeginHorizontal();
            if (tool.replacePrefab != null) {
                EditorGUILayout.TextField(tool.replacePrefab.name);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < tool.interfaceGoList.Count; i++) {
                FInterfaceData toolInterfaceData = tool.interfaceGoList[i];
                if (GUILayout.Button(toolInterfaceData.interfaceName, GUILayout.Height(30))) {
                    toolInterfaceData.interfaceGo.name = toolInterfaceData.interfaceName;
                    toolInterfaceData.interfaceGo.SetActive(true);

                    tool.replacePrefab = toolInterfaceData.interfaceGo;
                    Selection.activeObject = toolInterfaceData.interfaceGo;
                }
            }
            EditorGUILayout.EndHorizontal();
        }

    }

    private void InterfaceSave(FInterfaceEditorTool tool) {
        if (tool.isEditorInterface && tool.replacePrefab != null) {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("存储界面", GUILayout.Height(30))) {
                Save(tool, Selection.activeObject.name);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void Save(FInterfaceEditorTool tool, string interfaceName) {
        string settingPath = "Assets/Script/Framework/Setting/FInterfaceSetting.asset";
        FInterfaceSetting setting = AssetDatabase.LoadAssetAtPath<FInterfaceSetting>(settingPath);

        for (int i = 0; i < setting.interfacePrefabList.Count; i++) {
            FInterfaceData data = setting.interfacePrefabList[i];
            if (data.interfaceName.Equals(interfaceName)) {
                string path = AssetDatabase.GetAssetPath(data.interfaceGo);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                PrefabUtility.ReplacePrefab(tool.replacePrefab, prefab, ReplacePrefabOptions.ConnectToPrefab);

                AssetDatabase.SaveAssetIfDirty(setting);
                AssetDatabase.Refresh();
                tool.replacePrefab.SetActive(false);
                tool.replacePrefab = null;
                break;
            }
        }
    }
}