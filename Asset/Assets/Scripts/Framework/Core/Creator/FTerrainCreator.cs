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

    public void DestroyTerrain() {
        FGameManager.Instance.FGameMessage.Dis(FMessageCode.RemoveAllTerrain);
    }
}