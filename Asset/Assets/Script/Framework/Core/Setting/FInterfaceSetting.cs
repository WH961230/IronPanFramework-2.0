using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FInterfaceSetting", menuName = "FSetting/FInterfaceSetting", order = 1)]
public class FInterfaceSetting : ScriptableObject {
    public GameObject interfaceRoot;
    public List<FInterfaceData> interfacePrefabList;
}

[Serializable]
public class FInterfaceData {
    public string interfaceName;
    public GameObject interfaceGo;
} 