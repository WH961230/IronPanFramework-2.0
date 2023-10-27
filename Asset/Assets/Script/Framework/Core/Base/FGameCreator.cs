using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FGameCreator {
    private int IDCreator;
    private FCreatorSetting creatorSetting;
    private string settingPath = "Assets/Script/Framework/Setting/FCreatorSetting.asset"; 

    private FTerrainCreator FTerrainCreator;
    private FPlayerCreator FPlayerCreator;
    private FComponentCreator FComponentCreator;
    private FInterfaceCreator FInterfaceCreator;

    public FGameCreator() {
        creatorSetting = AssetDatabase.LoadAssetAtPath<FCreatorSetting>(settingPath);

        FTerrainCreator = new FTerrainCreator(this);
        FPlayerCreator = new FPlayerCreator(this);
        FComponentCreator = new FComponentCreator(this);
        FInterfaceCreator = new FInterfaceCreator(this);
    }

    public int GetIDCreator() {
        return ++IDCreator;
    }

    public void CreateGame() {
        CreateComponent();
        CreateInterface();
        CreateTerrain();
        CreatePlayer();
    }

    public void CreateComponent() {
        if (creatorSetting.IsReadArchive) {
            FComponentCreator.ReadArchive();
        }
        FComponentCreator.CreateComponent();
    }

    public void CreateInterface() {
        if (creatorSetting.IsReadArchive) {
            FInterfaceCreator.ReadArchive();
        }
        FInterfaceCreator.CreateInterface();
    }

    public void CreateTerrain() {
        if (creatorSetting.IsReadArchive) {
            FTerrainCreator.ReadArchive();
        }
        FTerrainCreator.CreateTerrain();
    }

    public void CreatePlayer() {
        if (creatorSetting.IsReadArchive) {
            FPlayerCreator.ReadArchive();
        }
        FPlayerCreator.CreateAllPlayer();
    }

    public void DestroyGame() {
        FPlayerCreator.DestroyPlayer();
        FTerrainCreator.DestroyTerrain();
        FInterfaceCreator.DestroyInterface();
        FComponentCreator.DestroyComponent();
    }
}