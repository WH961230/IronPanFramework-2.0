using UnityEditor;
using UnityEngine;

public class FComponentCreator {
    private string settingPath = "Assets/Script/Framework/Setting/FComponentSetting.asset"; 
    private FComponentSetting componentSetting;

    private FGameCreator gameCreator;
    public FComponentCreator(FGameCreator gameCreator) {
        this.gameCreator = gameCreator;
        componentSetting = AssetDatabase.LoadAssetAtPath<FComponentSetting>(settingPath);
    }

    public void ReadArchive() {
    }

    public void CreateComponent() {
    }

    public void DestroyComponent() {
    }
}