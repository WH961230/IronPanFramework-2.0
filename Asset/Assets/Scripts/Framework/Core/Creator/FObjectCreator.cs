using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FObjectCreator {
    private string settingPath = "Assets/Script/Framework/Setting/FObjectSetting.asset"; 
    private FObjectSetting objectSetting;

    private FPlayerCreator playerCreator;

    private FGameCreator gameCreator;
    public FObjectCreator(FGameCreator gameCreator) {
        this.gameCreator = gameCreator;
        objectSetting = AssetDatabase.LoadAssetAtPath<FObjectSetting>(settingPath);

        playerCreator = new FPlayerCreator(gameCreator, this);
    }

    public void ReadArchive() {
    }

    public List<FGameData.FObjectData> CreateAllObject() {
        List<FGameData.FPlayerData> fPlayerDatas = playerCreator.CreateAllPlayer();

        List<FGameData.FObjectData> fObjectDatas = new List<FGameData.FObjectData>();
        for (int i = 0; i < fPlayerDatas.Count; i++) {
            CreateSingleObject(fPlayerDatas[i]);
            fObjectDatas.Add(fPlayerDatas[i]);
        }

        return fObjectDatas;
    }

    private void CreateSingleObject(FGameData.FObjectData objectData) {
        FGameManager.Instance.FGameMessage.Dis(FMessageCode.CreateObject, objectData);
    }

    public void DestroyObject() {
        playerCreator.DestroyPlayer();
        FGameManager.Instance.FGameMessage.Dis(FMessageCode.RemoveAllObject);
    }
}