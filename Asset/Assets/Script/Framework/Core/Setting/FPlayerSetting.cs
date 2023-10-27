using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FPlayerSetting", menuName = "FSetting/FPlayerSetting", order = 1)]
public class FPlayerSetting : ScriptableObject {
    public string SettingName;
    public FPointToolSetting FPointToolSetting;
    public GameObject objectRootPrefab;
    public List<FPlayerData> playerDatas = new List<FPlayerData>();

    [Serializable]
    public class FPlayerData {
        public string name;
        public GameObject prefab;
    }
}