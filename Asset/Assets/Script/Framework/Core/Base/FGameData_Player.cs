using System.Collections.Generic;
using UnityEngine;

public partial class FGameData {
    private readonly List<FPlayerData> playerDatas = new List<FPlayerData>();
    private readonly Dictionary<int, FPlayerData> playerDataDics = new Dictionary<int, FPlayerData>();

    private void OnPlayerCreate(FPlayerData data) {
        bool tryAdd = playerDataDics.TryAdd(data.ID, data);
        if (tryAdd) {
            playerDatas.Add(data);
            Debug.Log($"创建玩家 {data.ID} 成功！");
        }
    }

    private void OnAllPlayerRemove() {
        for (int i = playerDatas.Count - 1; i >= 0; i--) {
            FPlayerData fPlayerData = playerDatas[i];
            OnPlayerRemove(fPlayerData.ID);
        }
        playerDataDics.Clear();
        playerDatas.Clear();
        Debug.Log($"移除所有玩家成功！");
    }

    private void OnPlayerRemove(int id) {
        bool containsKey = playerDataDics.ContainsKey(id);
        int index = playerDatas.FindIndex(data => data.ID == id);
        if (containsKey) {
            Object.DestroyImmediate(playerDatas[index].GO);
            playerDatas.RemoveAt(index);
            playerDataDics.Remove(id);
            Debug.Log($"移除玩家 {id} 成功！");
        }
    }

    public class FPlayerData {
        public int ID;
        public GameObject GO;
        public FGameObject FGO;
        public bool IsMainPlayer;
    }
}