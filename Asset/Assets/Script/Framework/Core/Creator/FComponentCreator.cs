using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class FComponentCreator {
    private string settingPath = "Assets/Script/Framework/Setting/FComponentSetting.asset"; 
    private FComponentSetting setting;

    private FGameCreator gameCreator;
    public FComponentCreator(FGameCreator gameCreator) {
        this.gameCreator = gameCreator;
        setting = AssetDatabase.LoadAssetAtPath<FComponentSetting>(settingPath);
        FGameMessage.Instance.Reg<FGameDataType, int, string>(FMessageCode.CreateComponent, Create);
        FGameMessage.Instance.Reg<int>(FMessageCode.RemoveComponent, Remove);
        FGameMessage.Instance.Reg(FMessageCode.RemoveAllComponent, RemoveAll);
    }

    private void Create(FGameDataType type, int id, string str) {
        FGameData.FComponentData compData = new FGameData.FComponentData();
        compData.RoleID = id;
        GameObject GO = null;
        switch (type) {
            case FGameDataType.Terrain:
                if (FGameData.Instance.TryGetTerrain(id, out FGameData.FTerrainData tData)) {
                    GO = tData.GO;
                }
                break;
            case FGameDataType.Player:
                if (FGameData.Instance.TryGetPlayer(id, out FGameData.FPlayerData pData)) {
                    GO = pData.GO;
                }
                break;
            case FGameDataType.Interface:
                if (FGameData.Instance.TryGetInterface(id, out FGameData.FInterfaceData iData)) {
                    GO = iData.GO;
                }
                break;
        }
        string[] parts = str.Split('_');

        Type classType = Type.GetType(parts[0]);
        var instance = Activator.CreateInstance(classType, new object[]{GO});

        MethodInfo methodInfo = classType.GetMethod(parts[1]);
        UnityAction result = (UnityAction)methodInfo.CreateDelegate(typeof(UnityAction), instance);

        compData.FixedUpdateActionList.Add(result);
        FGameMessage.Instance.Dis(FMessageCode.CreateComponentData, compData);
    }

    private void Remove(int id) {
        FGameMessage.Instance.Dis(FMessageCode.RemovePlayerData, id);
    }

    private void RemoveAll() {
        FGameMessage.Instance.Dis(FMessageCode.RemoveAllComponentData);

        FGameMessage.Instance.UnReg<FGameDataType, int, string>(FMessageCode.CreateComponent, Create);
        FGameMessage.Instance.UnReg<int>(FMessageCode.RemoveComponent, Remove);
        FGameMessage.Instance.UnReg(FMessageCode.RemoveAllComponent, RemoveAll);
    }
}