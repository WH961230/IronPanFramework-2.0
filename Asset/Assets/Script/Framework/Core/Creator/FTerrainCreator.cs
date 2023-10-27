using UnityEditor;
using UnityEngine;

public class FTerrainCreator {
    private string settingPath = "Assets/Script/Framework/Setting/FTerrainSetting.asset";
    private FTerrainSetting terrainSetting;

    private FGameCreator gameCreator;

    public FTerrainCreator(FGameCreator gameCreator) {
        this.gameCreator = gameCreator;
        terrainSetting = AssetDatabase.LoadAssetAtPath<FTerrainSetting>(settingPath);
        CreateRoot();
    }

    public void ReadArchive() {
    }

    public void CreateTerrain() {
        FGameData.FTerrainData data = new FGameData.FTerrainData();
        data.ID = gameCreator.GetIDCreator();
        data.GO = Object.Instantiate(terrainSetting.TerrainPrefab, GameObject.Find("FTerrainRoot")?.transform);

        FGameManager.Instance.FGameMessage.Dis(FMessageCode.CreateTerrain, data);
    }

    public void DestroyTerrain() {
        FGameManager.Instance.FGameMessage.Dis(FMessageCode.RemoveAllTerrain);
        RemoveRoot();
    }

    private GameObject CreateRoot() {
        GameObject rootGo = GameObject.Find("FTerrainRoot");
        if (rootGo != null) {
            return rootGo;
        }
        rootGo = Object.Instantiate(terrainSetting.TerrainRoot);
        rootGo.name = "FTerrainRoot";
        return rootGo;
    }

    private void RemoveRoot() {
        GameObject go = GameObject.Find("FTerrainRoot");
        if (go != null) {
            Object.DestroyImmediate(go);
        }
    }
}