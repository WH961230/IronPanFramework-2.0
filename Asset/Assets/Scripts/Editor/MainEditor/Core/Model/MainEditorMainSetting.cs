using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MainEditorMainSetting", menuName = "ScriptableObjects/MainEditor/MainEditorMainSetting", order = 1)]
public class MainEditorMainSetting : MainEditorSetting {
    public string GameManagerName;
    public string AudioRootName;
    public string TerrainRootName;
    public string ObjectRootName;
    public string CameraRootName;
    public string LightRootName;
    public string InterfaceRootName;

    public override void OnGUI() {
        base.OnGUI();
        GameManagerName = EditorGUILayout.TextField("游戏管理器命名", GameManagerName);

        EditorGUILayout.BeginHorizontal();
        AudioRootName = EditorGUILayout.TextField("音频根节点命名", AudioRootName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        TerrainRootName = EditorGUILayout.TextField("地形根节点命名", TerrainRootName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        ObjectRootName = EditorGUILayout.TextField("物体根节点命名", ObjectRootName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        CameraRootName = EditorGUILayout.TextField("相机根节点命名", CameraRootName);
        EditorGUILayout.EndHorizontal();
 
        EditorGUILayout.BeginHorizontal();
        LightRootName = EditorGUILayout.TextField("灯光根节点命名", LightRootName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        InterfaceRootName = EditorGUILayout.TextField("界面根节点命名", InterfaceRootName);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("创建根节点")) {
            CreateGameManager(GameManagerName);
            CreateRoot(AudioRootName);
            CreateRoot(TerrainRootName);
            CreateRoot(ObjectRootName);
            CreateRoot(CameraRootName);
            CreateRoot(LightRootName);
            CreateCanvas(InterfaceRootName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    void CreateGameManager(string name) {
        GameObject exitGo = GameObject.Find(name);
        if (string.IsNullOrEmpty(name) || exitGo != null) {
            return;
        }
        GameObject go = new GameObject(name);
        go.AddComponent<GameManager>();
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
    }

    void CreateRoot(string name) {
        GameObject exitGo = GameObject.Find(name);
        if (string.IsNullOrEmpty(name) || exitGo != null) {
            return;
        }
        GameObject go = new GameObject(name);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
    }

    void CreateCanvas(string name) {
        GameObject exitGo = GameObject.Find(name);
        if (string.IsNullOrEmpty(name) || exitGo != null) {
            return;
        }
        GameObject canvas = new GameObject(name);
        canvas.transform.localPosition = Vector3.zero;
        canvas.transform.localRotation = Quaternion.identity;
        canvas.layer = LayerMask.NameToLayer("UI");

        var canvasComp = canvas.transform.AddComponent<Canvas>();
        canvasComp.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.transform.AddComponent<CanvasScaler>();
        canvas.transform.AddComponent<GraphicRaycaster>();

        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
        eventSystem.transform.SetParent(canvas.transform);
    }
}