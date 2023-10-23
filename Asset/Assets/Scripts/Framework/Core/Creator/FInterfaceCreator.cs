using UnityEditor;
using UnityEngine;

public class FInterfaceCreator {
    private string settingPath = "Assets/Script/Framework/Setting/FInterfaceSetting.asset"; 
    private FInterfaceSetting InterfaceSetting;

    private FGameCreator gameCreator;
    public FInterfaceCreator(FGameCreator gameCreator) {
        this.gameCreator = gameCreator;
        InterfaceSetting = AssetDatabase.LoadAssetAtPath<FInterfaceSetting>(settingPath);
    }

    public void ReadArchive() {
    }

    public void CreateInterface() {
    }

    public void DestroyInterface() {
    }
}