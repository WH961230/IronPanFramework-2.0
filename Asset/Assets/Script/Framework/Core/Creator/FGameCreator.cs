using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FGameCreator {
    private int IDCreator;
    private FCreatorSetting setting;
    private string settingPath = "Assets/Script/Framework/Setting/FCreatorSetting.asset"; 

    public FGameCreator() {
        setting = AssetDatabase.LoadAssetAtPath<FCreatorSetting>(settingPath);
        new FTerrainCreator(this);
        new FPlayerCreator(this);
        new FComponentCreator(this);
        new FInterfaceCreator(this);
        FGameMessage.Instance.Reg(FMessageCode.CreateAll, CreateGame);
        FGameMessage.Instance.Reg(FMessageCode.DestoryAll, DestroyGame);
    }

    public int GetIDCreator() {
        return ++IDCreator;
    }

    private void CreateGame() {
        FGameMessage.Instance.Dis(FMessageCode.CreateInterface, "Image");
        FGameMessage.Instance.Dis(FMessageCode.CreateTerrain, "Terrain");
        FGameMessage.Instance.Dis(FMessageCode.CreatePlayer, "Player");
        FGameMessage.Instance.Dis(FMessageCode.CreatePlayer, "Item");
    }

    private void DestroyGame() {
        FGameMessage.Instance.Dis(FMessageCode.RemoveAllPlayer);
        FGameMessage.Instance.Dis(FMessageCode.RemoveAllTerrain);
        FGameMessage.Instance.Dis(FMessageCode.RemoveAllInterface);
        FGameMessage.Instance.Dis(FMessageCode.RemoveAllComponent);

        FGameMessage.Instance.UnReg(FMessageCode.CreateAll, CreateGame);
        FGameMessage.Instance.UnReg(FMessageCode.DestoryAll, DestroyGame);
    }
}