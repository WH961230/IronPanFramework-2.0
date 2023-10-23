using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FGameManager))]
public class FGameManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        FGameManager fGameManager = (FGameManager) target;
        if (GUILayout.Button("开始游戏")) {
            fGameManager.Awake();
            fGameManager.Start();
            fGameManager.FGameMessage.Dis(FMessageCode.StartGame);
        }

        if (GUILayout.Button("结束游戏")) {
            fGameManager.FGameMessage.Dis(FMessageCode.QuitGame);
        }
    }
}