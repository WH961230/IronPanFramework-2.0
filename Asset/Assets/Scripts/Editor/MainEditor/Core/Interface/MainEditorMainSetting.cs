using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorMainSetting", menuName = "ScriptableObjects/MainEditor/MainEditorMainSetting", order = 1)]
public class MainEditorMainSetting : MainEditorSetting {
    public string TerrainRootName;
    public string ObjectRootName;
    public string InterfaceRootName;

    public override void OnGUI() {
        base.OnGUI();
        EditorGUILayout.BeginHorizontal();
        TerrainRootName = EditorGUILayout.TextField("地形根节点命名", TerrainRootName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        ObjectRootName = EditorGUILayout.TextField("物体根节点命名", ObjectRootName);
        EditorGUILayout.EndHorizontal();
 
        EditorGUILayout.BeginHorizontal();
        InterfaceRootName = EditorGUILayout.TextField("界面根节点命名", InterfaceRootName);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("创建根节点")) {
            MainEditorManager.TerrainRoot = CreateRoot(TerrainRootName);
            MainEditorManager.ObjectRoot = CreateRoot(ObjectRootName);
            MainEditorManager.InterfaceRoot = CreateRoot(InterfaceRootName);
        }
    }

    GameObject CreateRoot(string name) {
        if (string.IsNullOrEmpty(name)) {
            return null;
        }
        GameObject go = new GameObject(name);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        return go;
    }
}