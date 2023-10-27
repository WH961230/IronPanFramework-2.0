using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FInterfaceCreator {
    private string settingPath = "Assets/Script/Framework/Setting/FInterfaceSetting.asset"; 
    private FInterfaceSetting setting;

    private FGameCreator gameCreator;
    public FInterfaceCreator(FGameCreator gameCreator) {
        this.gameCreator = gameCreator;
        setting = AssetDatabase.LoadAssetAtPath<FInterfaceSetting>(settingPath);
        CreateRoot("FInterfaceRoot");
    }

    public void ReadArchive() {
    }

    public void CreateInterface() {
        List<GameObject> interfaceGoList = new List<GameObject>(); 
        for (int i = 0; i < setting.interfacePrefabList.Count; i++) {
            GameObject prefab = setting.interfacePrefabList[i].interfaceGo;
            GameObject interfaceGo = Object.Instantiate(prefab, GameObject.Find("FInterfaceRoot")?.transform);

            interfaceGoList.Add(interfaceGo);
        }
    }

    public void DestroyInterface() {
        RemoveRoot("FInterfaceRoot");
    }

    private GameObject CreateRoot(string name) {
        GameObject interfaceGo = GameObject.Find(name);
        if (interfaceGo != null) {
            return interfaceGo;
        }
        GameObject interfaceRootGo = Object.Instantiate(setting.interfaceRoot);
        interfaceRootGo.name = name;

        return interfaceRootGo;
    }

    private static void RemoveRoot(string name) {
        GameObject interfaceGo = GameObject.Find(name);
        if (interfaceGo != null) {
            Object.DestroyImmediate(interfaceGo);
        }
    }
}