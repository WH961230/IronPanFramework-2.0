using System.Collections.Generic;
using UnityEngine;

public partial class FGameData {
    private readonly List<FTerrainData> terrainDatas = new List<FTerrainData>();
    private readonly Dictionary<int, FTerrainData> terrainDataDics = new Dictionary<int, FTerrainData>();

    private void OnTerrainCreate(FTerrainData data) {
        bool tryAdd = terrainDataDics.TryAdd(data.ID, data);
        if (tryAdd) {
            terrainDatas.Add(data);
            Debug.Log($"创建地形 {data.ID} 成功！");
        }
    }

    private void OnAllTerrainRemove() {
        for (int i = terrainDatas.Count - 1; i >= 0; i--) {
            FTerrainData fTerrainData = terrainDatas[i];
            OnTerrainRemove(fTerrainData.ID);
        }
        terrainDataDics.Clear();
        terrainDatas.Clear();
        Debug.Log($"移除所有地形成功！");
    }

    private void OnTerrainRemove(int id) {
        bool containsKey = terrainDataDics.ContainsKey(id);
        int index = terrainDatas.FindIndex(data => data.ID == id);
        if (containsKey) {
            Object.DestroyImmediate(terrainDatas[index].GO);
            terrainDataDics.Remove(id);
            terrainDatas.RemoveAt(index);
            Debug.Log($"移除地形 {id} 成功！");
        }
    }

    public class FTerrainData {
        public int ID;
        public GameObject GO;
    }
}