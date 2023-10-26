public partial class FGameData {
    public FGameData() {
        FGameManager.Instance.FGameMessage.Reg<FPlayerData>(FMessageCode.CreatePlayer, OnPlayerCreate);
        FGameManager.Instance.FGameMessage.Reg<int>(FMessageCode.RemovePlayer, OnPlayerRemove);
        FGameManager.Instance.FGameMessage.Reg(FMessageCode.RemoveAllPlayer, OnAllPlayerRemove);
        
        FGameManager.Instance.FGameMessage.Reg<FTerrainData>(FMessageCode.CreateTerrain, OnTerrainCreate);
        FGameManager.Instance.FGameMessage.Reg<int>(FMessageCode.RemoveTerrain, OnTerrainRemove);
        FGameManager.Instance.FGameMessage.Reg(FMessageCode.RemoveAllTerrain, OnAllTerrainRemove);
    }

    public void Clear() {
        FGameManager.Instance.FGameMessage.UnReg<FPlayerData>(FMessageCode.CreatePlayer, OnPlayerCreate);
        FGameManager.Instance.FGameMessage.UnReg<int>(FMessageCode.RemovePlayer, OnPlayerRemove);
        FGameManager.Instance.FGameMessage.UnReg(FMessageCode.RemoveAllPlayer, OnAllPlayerRemove);

        FGameManager.Instance.FGameMessage.UnReg<FTerrainData>(FMessageCode.CreateTerrain, OnTerrainCreate);
        FGameManager.Instance.FGameMessage.UnReg<int>(FMessageCode.RemoveTerrain, OnTerrainRemove);
        FGameManager.Instance.FGameMessage.UnReg(FMessageCode.RemoveAllTerrain, OnAllTerrainRemove);
    }
}