using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorTerrainSetting", menuName = "ScriptableObjects/MainEditor/MainEditorTerrainSetting", order = 1)]
public class MainEditorTerrainSetting : MainEditorSetting {
    public GameObject TerrainObject;
    public override void OnGUI() {
        base.OnGUI();
        TerrainObject = (GameObject)EditorGUILayout.ObjectField(TerrainObject, typeof(GameObject), true);
        if (GUILayout.Button("创建地形")) {
            CreateTerrain();
        }
    }
    
    void CreateTerrain() {
        if (TerrainObject == null) {
            return;
        }
        GameObject terrain = Instantiate(TerrainObject);
        terrain.name = "Terrain";
        GameObject root = GameObject.Find(GetMainSetting().TerrainRootName);
        terrain.transform.SetParent(root.transform);
        terrain.transform.localPosition = Vector3.zero;
        terrain.transform.localRotation = Quaternion.identity;
        terrain.transform.localScale = Vector3.one;
    }
}