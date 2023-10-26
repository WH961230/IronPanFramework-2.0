using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FGameManager))]
public class FGameManagerEditor : Editor {
    public override void OnInspectorGUI() {
        FGameManager fGameManager = (FGameManager)target;

        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");
        GUILayout.Label("游戏管理器");

        GameLifeCycle(fGameManager);
        GameMapEditor(fGameManager);

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
        }
        EditorGUILayout.EndHorizontal();
    }

    private void GameMapEditor(FGameManager fGameManager) {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("地图编辑器", GUILayout.Height(30))) {
            if (fGameManager.FGameState == FGameState.GameStart) {
                return;
            }
            FTerrainCreator terrainCreator = new FTerrainCreator(null);
            FMapEditorTool mapEditorTool = terrainCreator.CreateMapEditor(true, true);
            mapEditorTool.fGameManager = fGameManager;
            FEditorCommon.JumpToTarget(false, mapEditorTool);
        }

        EditorGUILayout.EndHorizontal();
    }
}