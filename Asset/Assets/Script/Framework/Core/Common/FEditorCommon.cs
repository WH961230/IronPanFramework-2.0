using System;
using UnityEditor;

public class FEditorCommon {
    public static void SaveScene() {
        UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
    }

    public static void JumpToTarget(bool isLock, UnityEngine.Object target) {
        LockInspector(isLock);
        Selection.activeObject = target;
    }

    public static void LockInspector(bool isLock) {
        Type t = typeof(EditorWindow).Assembly.GetType("UnityEditor.InspectorWindow");
        var window = EditorWindow.GetWindow(t);
        t.GetProperty("isLocked").SetValue(window, isLock);
    }
}