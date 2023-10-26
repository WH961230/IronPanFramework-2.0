using UnityEditor;
using UnityEngine;

public class FTerrainCreator {
    private string settingPath = "Assets/Script/Framework/Setting/FTerrainSetting.asset";
    private FTerrainSetting terrainSetting;

    private FGameCreator gameCreator;

    public FTerrainCreator(FGameCreator gameCreator) {
        this.gameCreator = gameCreator;
        terrainSetting = AssetDatabase.LoadAssetAtPath<FTerrainSetting>(settingPath);
    }

    public void ReadArchive() {
    }

    public void CreateTerrain() {
        FGameData.FTerrainData data = new FGameData.FTerrainData();
        data.ID = gameCreator.GetIDCreator();
        data.GO = Object.Instantiate(terrainSetting.TerrainPrefab);

        FGameManager.Instance.FGameMessage.Dis(FMessageCode.CreateTerrain, data);
    }

#if UNITY_EDITOR
    public FMapEditorTool CreateMapEditor(bool isCreate, bool isDisplay) {
        FMapEditorTool sceneMapEditor = Object.FindObjectOfType<FMapEditorTool>(true);
        if (sceneMapEditor == null) {
            if (isCreate) {
                GameObject mapEditor = new GameObject("地图编辑器");
                sceneMapEditor = mapEditor.AddComponent<FMapEditorTool>();
                mapEditor.transform.SetParent(GameObject.Find("FTerrain")?.transform);

                GameObject terrainGo = Object.Instantiate(terrainSetting.TerrainPrefab);
                terrainGo.transform.SetParent(sceneMapEditor.transform);
                sceneMapEditor.terrainGo = terrainGo;
            }
        }

        if (sceneMapEditor == null) {
            return null;
        }

        sceneMapEditor.gameObject.SetActive(isDisplay);
        return sceneMapEditor;
    }
#endif

    public void DestroyTerrain() {
        FGameManager.Instance.FGameMessage.Dis(FMessageCode.RemoveAllTerrain);
    }
}