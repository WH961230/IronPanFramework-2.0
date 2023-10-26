using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FPointToolSetting", menuName = "FSetting/ToolSetting/FPointToolSetting", order = 1)]
public class FPointToolSetting : ScriptableObject {
    public string FromSettingName;
    public ScriptableObject FromSetting;
    public GameObject FPointPrefab;
    public List<FPointData> FPointDatas = new List<FPointData>();

    public FPointData GetRandomFPointData() {
        if(FPointDatas.Count == 0) {
            return new FPointData();
        }
        return FPointDatas[UnityEngine.Random.Range(0, FPointDatas.Count)];
    }

    [Serializable]
    public class FPointData {
        public Vector3 FPointPos;
        public Quaternion FPointRot;
    }
}