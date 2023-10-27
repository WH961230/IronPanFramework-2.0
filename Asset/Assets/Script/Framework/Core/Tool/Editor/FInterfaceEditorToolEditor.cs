using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FInterfaceEditorTool))]
public class FInterfaceEditorToolEditor : Editor {
    public override void OnInspectorGUI() {
        FInterfaceEditorTool tool = (FInterfaceEditorTool)target;
        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");

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
        if (GUILayout.Button("编辑界面", GUILayout.Height(30))) {
            tool.isEditorInterface = !tool.isEditorInterface;
            FEditorCommon.LockInspector(tool.isEditorInterface);
        }
        EditorGUILayout.EndHorizontal();
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
                    tool.replacePrefab = toolInterfaceData.interfaceGo;
                    Selection.activeObject = toolInterfaceData.interfaceGo;
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        if (tool.isEditorInterface) {
            if (GUILayout.Button("存储地形", GUILayout.Height(30))) {
                Save(tool, Selection.activeObject.name);
            }
        }
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
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
                break;
            }
        }
    }
}