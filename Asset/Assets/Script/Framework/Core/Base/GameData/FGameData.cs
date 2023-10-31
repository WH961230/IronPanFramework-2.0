public partial class FGameData {
    public static FGameData Instance;
    public FGameData() {
        Instance = this;
        FGameMessage.Instance.Reg<FPlayerData>(FMessageCode.CreatePlayerData, OnPlayerCreate);
        FGameMessage.Instance.Reg<int>(FMessageCode.RemovePlayerData, OnPlayerRemove);
        FGameMessage.Instance.Reg(FMessageCode.RemoveAllPlayerData, OnAllPlayerRemove);

        FGameMessage.Instance.Reg<FTerrainData>(FMessageCode.CreateTerrainData, OnTerrainCreate);
        FGameMessage.Instance.Reg<int>(FMessageCode.RemoveTerrainData, OnTerrainRemove);
        FGameMessage.Instance.Reg(FMessageCode.RemoveAllTerrainData, OnAllTerrainRemove);

        FGameMessage.Instance.Reg<FComponentData>(FMessageCode.CreateComponentData, OnComponentCreate);
        FGameMessage.Instance.Reg<FComponentData>(FMessageCode.RemoveComponentData, OnComponentRemove);
        FGameMessage.Instance.Reg<int>(FMessageCode.RemoveAllComponentData, OnComponentRemoveAll);

        FGameMessage.Instance.Reg<FInterfaceData>(FMessageCode.CreateInterfaceData, OnInterfaceCreate);
        FGameMessage.Instance.Reg<int>(FMessageCode.RemoveInterfaceData, OnInterfaceRemove);
        FGameMessage.Instance.Reg(FMessageCode.RemoveAllInterfaceData, OnInterfaceRemoveAll);

        FGameMessage.Instance.Reg(FMessageCode.DestoryAllData, Clear);
    }

    private void Clear() {
        FGameMessage.Instance.UnReg<FPlayerData>(FMessageCode.CreatePlayerData, OnPlayerCreate);
        FGameMessage.Instance.UnReg<int>(FMessageCode.RemovePlayerData, OnPlayerRemove);
        FGameMessage.Instance.UnReg(FMessageCode.RemoveAllPlayerData, OnAllPlayerRemove);

        FGameMessage.Instance.UnReg<FTerrainData>(FMessageCode.CreateTerrainData, OnTerrainCreate);
        FGameMessage.Instance.UnReg<int>(FMessageCode.RemoveTerrainData, OnTerrainRemove);
        FGameMessage.Instance.UnReg(FMessageCode.RemoveAllTerrainData, OnAllTerrainRemove);

        FGameMessage.Instance.UnReg<FComponentData>(FMessageCode.CreateComponentData, OnComponentCreate);
        FGameMessage.Instance.UnReg<FComponentData>(FMessageCode.RemoveComponentData, OnComponentRemove);
        FGameMessage.Instance.UnReg<int>(FMessageCode.RemoveAllComponentData, OnComponentRemoveAll);

        FGameMessage.Instance.UnReg(FMessageCode.DestoryAllData, Clear);
    }
}

public enum FGameDataType {
    Player,
    Terrain,
    Component,
    Interface,
}