using System.Collections.Generic;
using UnityEngine;

public partial class FGameData {
    private readonly List<FInterfaceData> interfaceDatas = new List<FInterfaceData>();
    private readonly Dictionary<int, FInterfaceData> interfaceDataDics = new Dictionary<int, FInterfaceData>();

    public bool TryGetInterface(int id, out FInterfaceData data) {
        return interfaceDataDics.TryGetValue(id, out data);
    }

    private void OnInterfaceCreate(FInterfaceData data) {
        bool tryAdd = interfaceDataDics.TryAdd(data.ID, data);
        if (tryAdd) {
            interfaceDatas.Add(data);
            Debug.Log($"创建界面 {data.ID} 成功！");
        }
    }

    private void OnInterfaceRemoveAll() {
        for (int i = interfaceDatas.Count - 1; i >= 0; i--) {
            FInterfaceData fInterfaceData = interfaceDatas[i];
            OnInterfaceRemove(fInterfaceData.ID);
        }
        interfaceDataDics.Clear();
        interfaceDatas.Clear();
        Debug.Log($"移除所有界面成功！");
    }

    private void OnInterfaceRemove(int id) {
        bool containsKey = interfaceDataDics.ContainsKey(id);
        int index = interfaceDatas.FindIndex(data => data.ID == id);
        if (containsKey) {
            Object.DestroyImmediate(interfaceDatas[index].GO);
            interfaceDatas.RemoveAt(index);
            interfaceDataDics.Remove(id);
            Debug.Log($"移除界面 {id} 成功！");
        }
    }

    public class FInterfaceData {
        public int ID;
        public string NAME;
        public GameObject GO;
    }
}