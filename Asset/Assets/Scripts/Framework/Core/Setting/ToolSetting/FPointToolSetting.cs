using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "FPointToolSetting", menuName = "FSetting/ToolSetting/FPointToolSetting", order = 1)]
public class FPointToolSetting : ScriptableObject {
    public GameObject FPointToolProbe;
    public List<FPointData> FPointDatas = new List<FPointData>();

    public void CreatePointToolProbe() {
        
    }
    
    public void RemovePointToolProbe() {
        
    }

    [Serializable]
    public class FPointData {
        public Vector3 FPointPos;
        public Quaternion FPointRot;
    }

}