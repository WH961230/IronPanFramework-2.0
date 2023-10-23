using System.Collections.Generic;
using UnityEngine;

public partial class FGameData {
    private readonly List<FObjectData> objectDatas = new List<FObjectData>();
    private readonly Dictionary<int, FObjectData> objectDataDics = new Dictionary<int, FObjectData>();

    private void OnObjectCreate(FObjectData data) {
        bool tryAdd = objectDataDics.TryAdd(data.ID, data);
        if (tryAdd) {
            objectDatas.Add(data);
            Debug.Log($"创建物体 {data.ID} 成功！");
        }
    }

    private void OnAllObjectRemove() {
        for (int i = objectDatas.Count - 1; i >= 0; i--) {
            FObjectData fObjectData = objectDatas[i];
            OnObjectRemove(fObjectData.ID);
        }
        objectDataDics.Clear();
        objectDatas.Clear();
        Debug.Log($"移除所有物体成功！");
    }

    private void OnObjectRemove(int id) {
        bool containsKey = objectDataDics.ContainsKey(id);
        int index = objectDatas.FindIndex(data => data.ID == id);
        if (containsKey) {
            Object.DestroyImmediate(objectDatas[index].GO);
            objectDataDics.Remove(id);
            objectDatas.RemoveAt(index);
            Debug.Log($"移除物体 {id} 成功！");
        }
    }

    public class FObjectData {
        public int ID;
        public GameObject GO;
    }
}