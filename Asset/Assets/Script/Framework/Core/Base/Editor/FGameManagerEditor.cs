using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CustomEditor(typeof(FGameManager))]
public class FGameManagerEditor : Editor {
    public override void OnInspectorGUI() {
        FGameManager fGameManager = (FGameManager)target;

        GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Script/Framework/Setting/GUISkin/GUIBtn.guiskin");
        GUILayout.Label("游戏管理器");

        GameLifeCycle(fGameManager);
        GameInstallEditor(fGameManager);
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

    private void GameInstallEditor(FGameManager fGameManager) {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("安装基础", GUILayout.Height(30))) {
            Install("Interface");
            Install("Object");
            Install("Terrain");
            Install("Camera");
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

    private void Install(string type) {
        switch (type) {
            case "Interface":
                GameObject interfaceGo = GameObject.Find("FInterface");
                if (interfaceGo != null) {
                    DestroyImmediate(interfaceGo);
                    return;
                }
                GameObject canvasGo = new GameObject("FInterface");
                Canvas canvas = canvasGo.AddComponent<Canvas>();
                CanvasScaler canvasScaler = canvasGo.AddComponent<CanvasScaler>();
                GraphicRaycaster graphicRaycaster = canvasGo.AddComponent<GraphicRaycaster>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                GameObject eventSystemGo = new GameObject("EventSystem");
                EventSystem eventSystem = eventSystemGo.AddComponent<EventSystem>();
                StandaloneInputModule standaloneInputModule = eventSystemGo.AddComponent<StandaloneInputModule>();
                eventSystemGo.transform.SetParent(canvasGo.transform);
                break;
            case "Object":
                GameObject objectGo = GameObject.Find("FObject");
                if (objectGo != null) {
                    DestroyImmediate(objectGo);
                    return;
                }
                objectGo = new GameObject("FObject");
                break;
            case "Terrain":
                GameObject terrainGo = GameObject.Find("FTerrain");
                if (terrainGo != null) {
                    DestroyImmediate(terrainGo);
                    return;
                }
                terrainGo = new GameObject("FTerrain");
                break;
            case "Camera":
                GameObject cameraGo = GameObject.Find("FCamera");
                if (cameraGo != null) {
                    DestroyImmediate(cameraGo);
                    return;
                }
                cameraGo = new GameObject("FCamera");
                break;
        }
    }
}