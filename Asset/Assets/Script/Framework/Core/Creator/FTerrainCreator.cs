using UnityEditor;
using UnityEngine;

public class FTerrainCreator {
    private string settingPath = "Assets/Script/Framework/Setting/FTerrainSetting.asset";
    private FTerrainSetting setting;

    private FGameCreator gameCreator;

    public FTerrainCreator(FGameCreator gameCreator) {
        this.gameCreator = gameCreator;
        setting = AssetDatabase.LoadAssetAtPath<FTerrainSetting>(settingPath);
        CreateRoot();
        FGameMessage.Instance.Reg<string>(FMessageCode.CreateTerrain, Create);
        FGameMessage.Instance.Reg<int>(FMessageCode.RemovePlayer, Remove);
        FGameMessage.Instance.Reg(FMessageCode.RemoveAllTerrain, RemoveAll);
    }

    private void Create(string name) {
        for (int i = 0; i < setting.TerrainCreateInfos.Count; i++) {
            FTerrainSetting.FTerrainData tmpData = setting.TerrainCreateInfos[i];
            if (tmpData.name == name) {
                CreateSingle(tmpData);
                break;
            }
        }
    }

    private void CreateSingle(FTerrainSetting.FTerrainData settingData) {
        FGameData.FTerrainData data = new FGameData.FTerrainData();
        data.ID = gameCreator.GetIDCreator();
        data.GO = Object.Instantiate(settingData.prefab, GameObject.Find("FObjectRoot")?.transform);

        FPointToolSetting.FPointData tmpFPointData = setting.FPointToolSetting.GetRandomFPointData();
        data.GO.transform.position = tmpFPointData.FPointPos;
        data.GO.transform.rotation = tmpFPointData.FPointRot;
        FGameMessage.Instance.Dis(FMessageCode.CreateTerrainData, data);
    }

    private void Remove(int id) {
        FGameMessage.Instance.Dis(FMessageCode.RemoveTerrainData);
    }

    private void RemoveAll() {
        FGameMessage.Instance.Dis(FMessageCode.RemoveAllTerrainData);
        RemoveRoot();
    }

    private GameObject CreateRoot() {
        GameObject rootGo = GameObject.Find("FTerrainRoot");
        if (rootGo != null) {
            return rootGo;
        }
        rootGo = Object.Instantiate(setting.TerrainRoot);
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