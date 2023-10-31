using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class FGameData {
    private readonly List<FComponentData> componentDatas = new List<FComponentData>();
    private readonly Dictionary<int, List<FComponentData>> componentDataDics = new Dictionary<int, List<FComponentData>>();

    private void OnComponentCreate(FComponentData data) {
        if (componentDataDics.TryGetValue(data.RoleID, out List<FComponentData> listData)) {
            if (listData.Contains(data)) {
                return;
            }
            listData.Add(data);
            componentDataDics[data.RoleID] = listData;
        } else {
            List<FComponentData> tmpList = new List<FComponentData>();
            tmpList.Add(data);
            componentDataDics.Add(data.RoleID, tmpList);
        }

        for (int i = 0; i < data.FixedUpdateActionList.Count; i++) {
            FGameMessage.Instance.Dis(FMessageCode.AddUpdateListener, FUpdateType.FixedUpdate,
                data.FixedUpdateActionList[i]);
        }
    }

    private void OnComponentRemove(FComponentData data) {
        if (componentDataDics.TryGetValue(data.RoleID, out List<FComponentData> listData)) {
            if (listData.Contains(data)) {
                for (int i = 0; i < data.FixedUpdateActionList.Count; i++) {
                    FGameMessage.Instance.Dis(FMessageCode.RemoveUpdateListener, FUpdateType.FixedUpdate,
                        data.FixedUpdateActionList[i]);
                }
                listData.Remove(data);
                componentDataDics[data.RoleID] = listData;
            }
        }
    }

    private void OnComponentRemoveAll(int roleId) {
        if (componentDataDics.TryGetValue(roleId, out List<FComponentData> listData)) {
            for (int i = 0; i < listData.Count; i++) {
                FComponentData data = listData[i];
                for (int j = 0; j < data.FixedUpdateActionList.Count; j++) {
                    UnityAction action = data.FixedUpdateActionList[i];
                    FGameMessage.Instance.Dis(FMessageCode.RemoveUpdateListener, FUpdateType.FixedUpdate, action);
                }
            }
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