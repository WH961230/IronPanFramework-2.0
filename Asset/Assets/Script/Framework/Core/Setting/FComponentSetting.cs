using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FComponentSetting", menuName = "FSetting/FComponentSetting", order = 1)]
public class FComponentSetting : ScriptableObject {
    public List<FComponentData> componentDatas = new List<FComponentData>();

    [Serializable]
    public class FComponentData {
        public string name;
        public FComponentSetting setting;
    }
}