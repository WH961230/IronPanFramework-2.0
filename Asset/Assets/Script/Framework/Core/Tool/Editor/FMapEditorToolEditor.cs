using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FMapEditorTool))]
public class FMapEditorToolEditor : Editor {
    private bool isEditor = false;

    private Vector2 scrollPosition;
    private int lineContain;
    private int y = 0;
    private int x = 0;
    private int counter = 0;
    
    private string path;
    private string editorPrefabPath = "Assets/Script/Framework/Prefab/Terrain/";

    public override void OnInspectorGUI() {
        FMapEditorTool fMapEditorTool = (FMapEditorTool)target;
        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("返回", GUILayout.Height(30))) {
            DestroyImmediate(fMapEditorTool.gameObject);
            FEditorCommon.SaveScene();
            FEditorCommon.JumpToTarget(false, fMapEditorTool.fGameManager);
            return;
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("游戏管理器 - 地图编辑器");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("编辑地形", GUILayout.Height(30))) {
            isEditor = !isEditor;
            FEditorCommon.LockInspector(isEditor);
        }

        EditorGUILayout.EndHorizontal();

        GameSetting();
        GameEditor(fMapEditorTool);

        base.OnInspectorGUI();
    }

    private void GameSetting() {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("玩家配置", GUILayout.Height(30))) {
            FPlayerSetting fPlayerSetting =
                AssetDatabase.LoadAssetAtPath<FPlayerSetting>("Assets/Script/Framework/Setting/FPlayerSetting.asset");

            FEditorCommon.JumpToTarget(false, fPlayerSetting);
        }

        if (GUILayout.Button("地形配置", GUILayout.Height(30))) {
            FTerrainSetting fTerrainSetting =
                AssetDatabase.LoadAssetAtPath<FTerrainSetting>("Assets/Script/Framework/Setting/FTerrainSetting.asset");

            FEditorCommon.JumpToTarget(false, fTerrainSetting);
        }

        EditorGUILayout.EndHorizontal();
    }

    private void GameEditor(FMapEditorTool fMapEditorTool) {
        if (isEditor) {
            x = 0;
            y = 0;
            counter = 0;
            lineContain = Mathf.FloorToInt(EditorGUIUtility.currentViewWidth / 65f);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            EditorGUILayout.BeginHorizontal();
            editorPrefabPath = GUILayout.TextField(editorPrefabPath);
            
            if (editorPrefabPath.IndexOf("Assets") != 0) {
                editorPrefabPath = editorPrefabPath.Substring(editorPrefabPath.IndexOf("Assets")) + "/";
            }

            if (GUILayout.Button("浏览", GUILayout.Width(50f))) {
                editorPrefabPath = EditorUtility.OpenFolderPanel("窗口标题", Application.dataPath, "Assets/Script/Framework/Prefab/");
            }
            EditorGUILayout.EndHorizontal();

            string[] guids = AssetDatabase.FindAssets("t:prefab", new[] {editorPrefabPath});

            if (guids.Length > 0) {
                while (counter < guids.Length) {
                    DisplayPrefabItemLine(fMapEditorTool, guids);
                    x++;
                    y = 0;
                }
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("存储地形", GUILayout.Height(30))) {
                SaveTerrain(fMapEditorTool);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void DisplayPrefabItemLine(FMapEditorTool fMapEditorTool, string[] guids) {
        GUILayout.BeginHorizontal();

        while (y < lineContain) {
            int index = x * lineContain + y;
            if(index >= guids.Length) {
                break;
            }

            DisplayPrefabItem(fMapEditorTool, guids[index]);
            y++;
            counter++;
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DisplayPrefabItem(FMapEditorTool fMapEditorTool, string guid) {
        string guidToAssetPath = AssetDatabase.GUIDToAssetPath(guid);
        GameObject go = (GameObject)AssetDatabase.LoadMainAssetAtPath(guidToAssetPath);

        EditorGUILayout.BeginVertical(GUILayout.Width(60));
        Texture2D thumbnail = AssetPreview.GetAssetPreview(go);
        if(GUILayout.Button(thumbnail, GUILayout.Width(60), GUILayout.Height(60))) {
            GameObject spawnedPrefab = Instantiate(go);
            spawnedPrefab.transform.position = Vector3.zero;
            spawnedPrefab.transform.rotation = Quaternion.identity;
            spawnedPrefab.transform.SetParent(fMapEditorTool.terrainGo.transform);
            Selection.activeObject = spawnedPrefab;
        }
        EditorGUILayout.TextField(go.name, EditorStyles.miniLabel, GUILayout.Width(60));
        EditorGUILayout.EndVertical();
    }

    private void SaveTerrain(FMapEditorTool fMapEditorTool) {
        string settingPath = "Assets/Script/Framework/Setting/FTerrainSetting.asset";
        FTerrainSetting terrainSetting = AssetDatabase.LoadAssetAtPath<FTerrainSetting>(settingPath);

        string path = AssetDatabase.GetAssetPath(terrainSetting.TerrainPrefab);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        fMapEditorTool.terrainGo.name = "FTerrain";
        GameObject replacePrefab = PrefabUtility.ReplacePrefab(fMapEditorTool.terrainGo, prefab, ReplacePrefabOptions.ConnectToPrefab);

        terrainSetting.TerrainPrefab = replacePrefab;

        AssetDatabase.SaveAssetIfDirty(terrainSetting);
        AssetDatabase.Refresh();
    }
}