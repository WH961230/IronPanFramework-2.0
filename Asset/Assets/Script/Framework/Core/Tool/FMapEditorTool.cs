using UnityEditor;
using UnityEngine;

public class FMapEditorTool : MonoBehaviour {
    public FGameManager fGameManager;
    public GameObject terrainGo;

    public FPlayerSetting FPlayerSetting {
        get {
            return AssetDatabase.LoadAssetAtPath<FPlayerSetting>(
                "Assets/Script/Framework/Setting/FPlayerSetting.asset");
        }
    }

    public FTerrainSetting FTerrainSetting {
        get {
            return AssetDatabase.LoadAssetAtPath<FTerrainSetting>(
                "Assets/Script/Framework/Setting/FTerrainSetting.asset");
        }
    }
}