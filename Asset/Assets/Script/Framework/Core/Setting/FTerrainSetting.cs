using UnityEngine;

[CreateAssetMenu(fileName = "FTerrainSetting", menuName = "FSetting/FTerrainSetting", order = 1)]
public class FTerrainSetting : ScriptableObject {
    public string SettingName;
    public FPointToolSetting FPointToolSetting;
    public GameObject TerrainPrefab;
}