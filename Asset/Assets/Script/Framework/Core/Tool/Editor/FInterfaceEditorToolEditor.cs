using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FInterfaceEditorTool))]
public class FInterfaceEditorToolEditor : Editor {
    private string newGoName;

    const string settingPath = "Assets/Script/Framework/Setting/FInterfaceSetting.asset";
    private FInterfaceSetting setting;
    private FInterfaceSetting Setting {
        get {
            if (setting == null) {
                setting = AssetDatabase.LoadAssetAtPath<FInterfaceSetting>(settingPath);
            }
            return setting;
        }
    }

    public override void OnInspectorGUI() {
        FInterfaceEditorTool tool = (FInterfaceEditorTool)target;
        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");

        Back(tool);

        GUILayout.Label("游戏管理器 - 界面编辑器");

        InterfaceAdd(tool);
        InterfaceEdit(tool);

        base.OnInspectorGUI();
    }

    private void Back(FInterfaceEditorTool tool) {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("返回", GUILayout.Height(30))) {
            DestroyImmediate(tool.gameObject);
            FEditorCommon.SaveScene();
            FEditorCommon.JumpToTarget(false, tool.fGameManager);
            return;
        }
        EditorGUILayout.EndHorizontal();
    }

    private void InterfaceAdd(FInterfaceEditorTool tool) {
        if (GUILayout.Button(tool.isAddInterface ? "保存界面" : "输入并添加界面", GUILayout.Height(30))) {
            tool.isAddInterface = !tool.isAddInterface;
            if(!tool.isAddInterface){
                Selection.activeObject = tool.transform.gameObject;
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (tool.isAddInterface) {
            EditorGUILayout.BeginVertical();

            newGoName = GUILayout.TextField(newGoName, GUILayout.Height(30));

            if (GUILayout.Button("添加界面", GUILayout.Height(30))) {
                if (!string.IsNullOrEmpty(newGoName)) {
                    GameObject newGo = new GameObject(newGoName);
                    newGo.transform.AddComponent<CanvasRenderer>();
                    newGo.transform.SetParent(tool.transform);
                    newGo.transform.localPosition = Vector3.zero;
                    newGo.transform.localRotation = Quaternion.identity;
                    GameObject go = PrefabUtility.SaveAsPrefabAsset(newGo, $"Assets/Script/Framework/Prefab/Interface/{newGoName}.prefab");
                    Setting.interfacePrefabList.Add(new FInterfaceData() {
                        interfaceName = newGoName,
                        interfaceGo = go
                    });
                    DestroyImmediate(newGo);

                    Selection.activeObject = newGo;
                }
            }
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndHorizontal();
    }

    private void InterfaceEdit(FInterfaceEditorTool tool) {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("编辑界面", GUILayout.Height(30))) {
            tool.isEditorInterface = !tool.isEditorInterface;
            if (!tool.isEditorInterface) {
                tool.replacePrefab = null;
                Selection.activeObject = tool;
            }
        }
        EditorGUILayout.EndHorizontal();
        InterfaceDisplay(tool);
        InterfaceSave(tool);
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
                    if (tool.replacePrefab == null) {
                        toolInterfaceData.interfaceGo.name = toolInterfaceData.interfaceName;
                        toolInterfaceData.interfaceGo.SetActive(true);

                        tool.replacePrefab = toolInterfaceData.interfaceGo;
                        Selection.activeObject = toolInterfaceData.interfaceGo;
                    } else {
                        toolInterfaceData.interfaceGo.SetActive(false);

                        tool.replacePrefab = null;
                        Selection.activeObject = tool;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void InterfaceSave(FInterfaceEditorTool tool) {
        if (tool.isEditorInterface && tool.replacePrefab != null) {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("存储界面", GUILayout.Height(30))) {
                Save(tool, tool.replacePrefab.name);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void Save(FInterfaceEditorTool tool, string interfaceName) {
        for (int i = 0; i < Setting.interfacePrefabList.Count; i++) {
            FInterfaceData data = Setting.interfacePrefabList[i];
            if (data.interfaceName.Equals(interfaceName)) {
                string path = AssetDatabase.GetAssetPath(data.interfaceGo);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                PrefabUtility.ReplacePrefab(tool.replacePrefab, prefab, ReplacePrefabOptions.ConnectToPrefab);

                AssetDatabase.SaveAssetIfDirty(Setting);
                AssetDatabase.Refresh();
                tool.replacePrefab.SetActive(false);
                tool.replacePrefab = null;
                break;
            }
        }
    }

    private void RefreshInterface() {
        
    }
}