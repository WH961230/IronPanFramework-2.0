using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class FGameData {
    private readonly List<FComponentData> componentDatas = new List<FComponentData>();
    private readonly Dictionary<int, List<FComponentData>> componentDataDics = new Dictionary<int, List<FComponentData>>();

    private void OnComponentAdd(FComponentData data) {
        if (componentDataDics.TryGetValue(data.RoleID, out List<FComponentData> listData)) {
            if (!listData.Contains(data)) {
                listData.Add(data);
                componentDataDics[data.RoleID] = listData;
            } else {
                return;
            }
        } else {
            List<FComponentData> tmpList = new List<FComponentData>();
            tmpList.Add(data);
            componentDataDics.Add(data.RoleID, tmpList);
        }

        for (int i = 0; i < data.FixedUpdateActionList.Count; i++) {
            FGameManager.Instance.FixedUpdateEvent.AddListener(data.FixedUpdateActionList[i]);
        }
    }

    private void OnComponentRemove(FComponentData data) {
        if (componentDataDics.TryGetValue(data.RoleID, out List<FComponentData> listData)) {
            if (listData.Contains(data)) {
                for (int i = 0; i < data.FixedUpdateActionList.Count; i++) {
                    FGameManager.Instance.FixedUpdateEvent.RemoveListener(data.FixedUpdateActionList[i]);
                }
                listData.Remove(data);
                componentDataDics[data.RoleID] = listData;
            }
        }
    }

    private void OnComponentRemoveAll(int roleId) {
        if (componentDataDics.TryGetValue(roleId, out List<FComponentData> listData)) {
            componentDataDics.Remove(roleId);
        }
    }

    public class FComponentData {
        public int RoleID;
        public GameObject GO;
        public List<UnityAction> UpdateActionList = new List<UnityAction>();
        public List<UnityAction> FixedUpdateActionList = new List<UnityAction>();
        public List<UnityAction> LateUpdateActionList = new List<UnityAction>();
    }
}