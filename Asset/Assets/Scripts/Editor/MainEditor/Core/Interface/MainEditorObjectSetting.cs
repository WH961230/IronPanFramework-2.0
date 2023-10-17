using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorObjectSetting", menuName = "ScriptableObjects/MainEditor/MainEditorObjectSetting", order = 1)]
public class MainEditorObjectSetting : MainEditorSetting {
    public GameObject ObjectPrefab;
    public override void OnGUI() {
        base.OnGUI();
        ObjectPrefab = (GameObject)EditorGUILayout.ObjectField(ObjectPrefab, typeof(GameObject), true);
        if (GUILayout.Button("创建物体")) {
            CreateObject();
        }
    }

    void CreateObject() {
        if (ObjectPrefab == null) {
            Debug.LogError("TerrainObject is null");
            return;
        }
        GameObject terrain = Instantiate(ObjectPrefab);
        terrain.name = "ObjectPrefab";
        terrain.transform.SetParent(MainEditorManager.ObjectRoot.transform);
        terrain.transform.localPosition = Vector3.zero;
        terrain.transform.localRotation = Quaternion.identity;
        terrain.transform.localScale = Vector3.one;
    }
}