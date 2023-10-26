using UnityEngine;

[CreateAssetMenu(fileName = "FCreatorSetting", menuName = "FSetting/FCreatorSetting", order = 1)]
public class FCreatorSetting : ScriptableObject {
    [Header("读取存档")] public bool IsReadArchive;
}