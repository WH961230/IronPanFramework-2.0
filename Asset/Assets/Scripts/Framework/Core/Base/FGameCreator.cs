using UnityEditor;

public class FGameCreator {
    private int IDCreator;
    private FCreatorSetting creatorSetting;
    private string settingPath = "Assets/Script/Framework/Setting/FCreatorSetting.asset"; 

    private FTerrainCreator FTerrainCreator;
    private FObjectCreator FObjectCreator;
    private FComponentCreator FComponentCreator;
    private FInterfaceCreator FInterfaceCreator;

    public FGameCreator() {
        creatorSetting = AssetDatabase.LoadAssetAtPath<FCreatorSetting>(settingPath);

        FTerrainCreator = new FTerrainCreator(this);
        FObjectCreator = new FObjectCreator(this);
        FComponentCreator = new FComponentCreator(this);
        FInterfaceCreator = new FInterfaceCreator(this);
    }

    public int GetIDCreator() {
        return ++IDCreator;
    }

    public void CreateGame() {
        if (creatorSetting.IsReadArchive) {
            FComponentCreator.ReadArchive();
            FInterfaceCreator.ReadArchive();
            FTerrainCreator.ReadArchive();
            FObjectCreator.ReadArchive();
        }

        FComponentCreator.CreateComponent();
        FInterfaceCreator.CreateInterface();
        FTerrainCreator.CreateTerrain();
        FObjectCreator.CreateAllObject();
    }

    public void DestroyGame() {
        FObjectCreator.DestroyObject();
        FTerrainCreator.DestroyTerrain();
        FInterfaceCreator.DestroyInterface();
        FComponentCreator.DestroyComponent();
    }
}