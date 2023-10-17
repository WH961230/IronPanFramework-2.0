using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorInterfaceSetting", menuName = "ScriptableObjects/MainEditor/MainEditorInterfaceSetting", order = 1)]
public class MainEditorInterfaceSetting : MainEditorSetting {
    public GameObject InterfacePrefab;
    public override void OnGUI() {
        base.OnGUI();
        InterfacePrefab = (GameObject)EditorGUILayout.ObjectField(InterfacePrefab, typeof(GameObject), true);
        if (GUILayout.Button("创建物体")) {
            CreateObject();
        }
    }

    void CreateObject() {
        if (InterfacePrefab == null) {
            Debug.LogError("TerrainObject is null");
            return;
        }
        GameObject terrain = Instantiate(InterfacePrefab);
        terrain.name = "InterfacePrefab";
        terrain.transform.SetParent(MainEditorManager.InterfaceRoot.transform);
        terrain.transform.localPosition = Vector3.zero;
        terrain.transform.localRotation = Quaternion.identity;
        terrain.transform.localScale = Vector3.one;
    }
}