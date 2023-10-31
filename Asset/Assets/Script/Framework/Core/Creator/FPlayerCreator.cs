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
        FGameMessage.Instance.Reg<string>(FMessageCode.CreatePlayer, Create);
        FGameMessage.Instance.Reg<int>(FMessageCode.RemovePlayer, Remove);
        FGameMessage.Instance.Reg(FMessageCode.RemoveAllPlayer, RemoveAll);
    }

    private void Create(string name) {
        for (int i = 0; i < playerSetting.playerDatas.Count; i++) {
            FPlayerSetting.FPlayerData tmpData = playerSetting.playerDatas[i];
            if (tmpData.name == name) {
                CreateSingle(tmpData);
                break;
            }
        }
    }

    private void CreateSingle(FPlayerSetting.FPlayerData settingPlayerData) {
        FGameData.FPlayerData data = new FGameData.FPlayerData();
        data.ID = gameCreator.GetIDCreator();
        data.GO = Object.Instantiate(settingPlayerData.prefab, GameObject.Find("FObjectRoot")?.transform);
        data.GO.name = settingPlayerData.name + "_" + data.ID;

        FPointToolSetting.FPointData tmpFPointData = playerSetting.FPointToolSetting.GetRandomFPointData();
        data.GO.transform.position = tmpFPointData.FPointPos;
        data.GO.transform.rotation = tmpFPointData.FPointRot;
        data.MAIN = true;
        FGameMessage.Instance.Dis(FMessageCode.CreatePlayerData, data);

        for (int i = 0; i < settingPlayerData.components.Count; i++) {
            string component = settingPlayerData.components[i];
            FGameMessage.Instance.Dis(FMessageCode.CreateComponent, FGameDataType.Player, data.ID, component);
        }
    }

    private void Remove(int id) {
        FGameMessage.Instance.Dis(FMessageCode.RemovePlayerData, id);
    }

    private void RemoveAll() {
        FGameMessage.Instance.Dis(FMessageCode.RemoveAllPlayerData);

        FGameMessage.Instance.UnReg<string>(FMessageCode.CreatePlayer, Create);
        FGameMessage.Instance.UnReg<int>(FMessageCode.RemovePlayer, Remove);
        FGameMessage.Instance.UnReg(FMessageCode.RemoveAllPlayer, RemoveAll);
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