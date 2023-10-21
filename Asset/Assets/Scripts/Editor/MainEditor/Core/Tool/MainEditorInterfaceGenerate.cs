using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class MainEditorInterfaceGenerate : EditorWindow {
    private static int currentProccess = 0;
    private static int totalCount = 0;
    private static string className = "";

    [MenuItem("Tools/MainEditorInterfaceGenerate _F4")]
    static void InitInterface() {
        EditorWindow window = GetWindow(typeof(MainEditorInterfaceGenerate), true, "Interface Generate");
        window.Show();
    }

    private void OnGUI() {
        className = EditorGUILayout.TextField("ClassName", className);
        if (GUILayout.Button("Generate")) {
            string inputPath = "Assets/Scripts/Editor/MainEditor/Config/Template/GenerateInterface.txt";
            string outputPath = "Assets/Scripts/Editor/MainEditor/Core/Model/";
            if (String.IsNullOrEmpty(className)) {
                return;
            }

            bool createScript = CreateStript(inputPath, outputPath, className, "MainEditor", "Setting");
            if (createScript) {
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("创建成功", $"创建标志：{className}", "确定");
            }
        }
    }

    private static bool CreateStript(string inputPath, string outputPath, string className, string front, string end) {
        if (inputPath.EndsWith(".txt")) {
            var streamReader = new StreamReader(inputPath);
            var log = streamReader.ReadToEnd();
            streamReader.Close();
            log = Regex.Replace(log, "#ClassName#", className);
            log = Regex.Replace(log, "#ClassParamName#", className.ToLower());
            var createPath = $"{outputPath}{front}{className}{end}.cs";
            var streamWriter = new StreamWriter(createPath, false, new UTF8Encoding(true, false));
            streamWriter.Write(log);
            streamWriter.Close();
            AssetDatabase.ImportAsset(createPath);
            ++currentProccess;
            EditorUtility.DisplayProgressBar("创建中 ...", "", (float) currentProccess / totalCount);
            return true;
        }

        return false;
    }
}