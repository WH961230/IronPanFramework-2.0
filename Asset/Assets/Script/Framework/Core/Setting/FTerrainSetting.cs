using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FTerrainSetting", menuName = "FSetting/FTerrainSetting", order = 1)]
public class FTerrainSetting : ScriptableObject {
    public string SettingName;
    public FPointToolSetting FPointToolSetting;
    public GameObject TerrainRoot;
    public List<FTerrainData> TerrainCreateInfos = new List<FTerrainData>();

    [Serializable]
    public class FTerrainData {
        public string name;
        public GameObject prefab;
    }
}