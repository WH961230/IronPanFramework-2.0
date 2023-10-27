using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FGameManager))]
public class FGameManagerEditor : Editor {
    public override void OnInspectorGUI() {
        FGameManager fGameManager = (FGameManager)target;

        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");
        GUILayout.Label("游戏管理器" + (fGameManager.FGameState == FGameState.GameStart ? " - 开始游戏" : ""));

        GameLifeCycle(fGameManager);

        if (fGameManager.FGameState != FGameState.GameStart) {
            GameInterfaceEditor(fGameManager);
            GameMapEditor(fGameManager);
        }

        base.OnInspectorGUI();
    }

    private void GameLifeCycle(FGameManager fGameManager) {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("开始游戏", GUILayout.Height(30))) {
            if (FGameManager.Instance == null) {
                fGameManager.Awake();
                fGameManager.Start();
            }

            fGameManager.FGameMessage.Dis(FMessageCode.StartGame);
        }

        if (GUILayout.Button("结束游戏", GUILayout.Height(30))) {
            fGameManager.FGameMessage.Dis(FMessageCode.QuitGame);
            FEditorCommon.SaveScene();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void GameInterfaceEditor(FGameManager fGameManager) {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("界面编辑器", GUILayout.Height(30))) {
            if (fGameManager.FGameState == FGameState.GameStart) {
                return;
            }

            FInterfaceEditorTool fInterfaceEditorTool = CreateInterfaceEditor();
            fInterfaceEditorTool.fGameManager = fGameManager;
            fInterfaceEditorTool.isEditorInterface = false;
            FEditorCommon.JumpToTarget(false, fInterfaceEditorTool);
        }

        EditorGUILayout.EndHorizontal();
    }

    private FInterfaceEditorTool CreateInterfaceEditor() {
        string settingPath = "Assets/Script/Framework/Setting/FInterfaceSetting.asset";
        FInterfaceSetting setting = AssetDatabase.LoadAssetAtPath<FInterfaceSetting>(settingPath);

        FInterfaceEditorTool sceneInterfaceEditor = FindObjectOfType<FInterfaceEditorTool>(true);
        if (sceneInterfaceEditor == null) {
            GameObject interfaceEditor = Instantiate(setting.interfaceRoot);
            interfaceEditor.name = "界面编辑器";

            sceneInterfaceEditor = interfaceEditor.AddComponent<FInterfaceEditorTool>();

            List<FInterfaceData> interfaceDataList = new List<FInterfaceData>(); 
            for (int i = 0; i < setting.interfacePrefabList.Count; i++) {
                FInterfaceData data = setting.interfacePrefabList[i];
                GameObject interfaceGo = Instantiate(data.interfaceGo, sceneInterfaceEditor.transform);
                interfaceGo.SetActive(false);
                interfaceDataList.Add(new FInterfaceData() {
                    interfaceName = data.interfaceName,
                    interfaceGo = interfaceGo,
                });
            }

            sceneInterfaceEditor.interfaceGoList = interfaceDataList;
        }
        return sceneInterfaceEditor;
    }

    private void GameMapEditor(FGameManager fGameManager) {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("地图编辑器", GUILayout.Height(30))) {
            if (fGameManager.FGameState == FGameState.GameStart) {
                return;
            }

            FMapEditorTool mapEditorTool = CreateMapEditor();
            mapEditorTool.fGameManager = fGameManager;
            FEditorCommon.JumpToTarget(false, mapEditorTool);
        }

        EditorGUILayout.EndHorizontal();
    }

    private FMapEditorTool CreateMapEditor() {
        string settingPath = "Assets/Script/Framework/Setting/FTerrainSetting.asset";
        FTerrainSetting setting = AssetDatabase.LoadAssetAtPath<FTerrainSetting>(settingPath);

        FMapEditorTool sceneMapEditor = FindObjectOfType<FMapEditorTool>(true);
        if (sceneMapEditor == null) {
            GameObject mapEditor = Instantiate(setting.TerrainRoot);
            mapEditor.name = "地图编辑器";

            sceneMapEditor = mapEditor.AddComponent<FMapEditorTool>();

            GameObject terrainGo = Instantiate(setting.TerrainPrefab, sceneMapEditor.transform);
            sceneMapEditor.terrainGo = terrainGo;
        }

        return sceneMapEditor;
    }
}