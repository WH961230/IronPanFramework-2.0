using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FPlayerCreator {
    private string settingPath = "Assets/Script/Framework/Setting/FPlayerSetting.asset"; 
    private FPlayerSetting playerSetting;

    private FGameCreator gameCreator;
    public FPlayerCreator(FGameCreator gameCreator) {
        this.gameCreator = gameCreator;
        playerSetting = AssetDatabase.LoadAssetAtPath<FPlayerSetting>(settingPath);
        CreateRoot();
    }

    public void ReadArchive() {
    }

    public List<FGameData.FPlayerData> CreateAllPlayer() {
        List<FGameData.FPlayerData> retDatas = new List<FGameData.FPlayerData>();
        for (int i = 0; i < playerSetting.playerDatas.Count; i++) {
            FPlayerSetting.FPlayerData tmpData = playerSetting.playerDatas[i];
            FGameData.FPlayerData tmpPlayerData = CreateSinglePlayer(tmpData);
            retDatas.Add(tmpPlayerData);
        }

        return retDatas;
    }

    private FGameData.FPlayerData CreateSinglePlayer(FPlayerSetting.FPlayerData settingPlayerData) {
        FGameData.FPlayerData data = new FGameData.FPlayerData();
        data.ID = gameCreator.GetIDCreator();
        data.GO = Object.Instantiate(settingPlayerData.prefab, GameObject.Find("FObjectRoot")?.transform);
        FPointToolSetting.FPointData tmpFPointData = playerSetting.FPointToolSetting.GetRandomFPointData();
        data.GO.transform.position = tmpFPointData.FPointPos;
        data.GO.transform.rotation = tmpFPointData.FPointRot;
        data.IsMainPlayer = true;
        FGameManager.Instance.FGameMessage.Dis(FMessageCode.CreatePlayer, data);
        return data;
    }

    public void DestroyPlayer() {
        FGameManager.Instance.FGameMessage.Dis(FMessageCode.RemoveAllPlayer);
        RemoveRoot();
    }

    private void CreateRoot() {
        GameObject rootGo = GameObject.Find("FObjectRoot");
        if (rootGo != null) {
            return;
        }
        rootGo = Object.Instantiate(playerSetting.objectRootPrefab);
        rootGo.name = "FObjectRoot";
    }

    private void RemoveRoot() {
        GameObject go = GameObject.Find("FObjectRoot");
        if (go != null) {
            Object.DestroyImmediate(go);
        }
    }
}