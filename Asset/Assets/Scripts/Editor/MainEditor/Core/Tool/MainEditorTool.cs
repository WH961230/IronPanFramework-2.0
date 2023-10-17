using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainEditorTool {
    public static T[] FindAllSetting<T>(string path) where T : ScriptableObject{
        var assetsId = AssetDatabase.FindAssets("t:scriptableobject", new[] {
            path
        });
        var retSettingList = new List<T>();
        for (var i = 0; i < assetsId.Length; i++) {
            var id = assetsId[i];
            var setting = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(id)) as T;
            if (setting == null) {
                continue;
            }
            retSettingList.Add(setting);
        }

        return retSettingList.ToArray();
    }
}