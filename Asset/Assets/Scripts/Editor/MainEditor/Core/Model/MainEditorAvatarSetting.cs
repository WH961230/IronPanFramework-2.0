using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorAvatarSetting", menuName = "ScriptableObjects/MainEditor/MainEditorAvatarSetting", order = 1)]
public class MainEditorAvatarSetting : MainEditorSetting {
    public Camera AvatarCamera;
    public RenderTexture RenderTexture;
    public string SavePath;
    public string SpriteSaveName;

    public override void OnGUI() {
        base.OnGUI();
        AvatarCamera = (Camera)EditorGUILayout.ObjectField(AvatarCamera, typeof(Camera), true);
        RenderTexture = (RenderTexture)EditorGUILayout.ObjectField(RenderTexture, typeof(RenderTexture), true);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("保存地址");
        SavePath = EditorGUILayout.TextField(SavePath);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("图片名称");
        SpriteSaveName = EditorGUILayout.TextField(SpriteSaveName);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("拍摄角色头像")) {
            TakeSnap();
        }
    }

    private void TakeSnap() {
        AvatarCamera.targetTexture = RenderTexture;
        //渲染相机渲染到目标纹理
        RenderTexture.active = RenderTexture;
        //创建一个新的纹理
        Texture2D texture2D = new Texture2D(RenderTexture.width, RenderTexture.height, TextureFormat.RGB24, false);
        //读取像素
        texture2D.ReadPixels(new Rect(0, 0, RenderTexture.width, RenderTexture.height), 0, 0);
        //应用
        texture2D.Apply();
        //重置
        RenderTexture.active = null;
        //保存
        byte[] bytes = texture2D.EncodeToPNG();
        string pngName = Time.realtimeSinceStartup.ToString();
        string path = SavePath + SpriteSaveName;
        System.IO.File.WriteAllBytes(path, bytes);
        //刷新
        AssetDatabase.Refresh();
        //设置为 Sprite
        texture2D = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        SetTextureTypeToSprite(texture2D);
        //读取 Sprite
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
    }

    //设置 Texture2D 类型为 Sprite
    private static void SetTextureTypeToSprite(Object textureObj) {
        // 将Texture转换为TextureImporter对象
        TextureImporter textureImporter =
            AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(textureObj)) as TextureImporter;
        if (textureImporter != null) {
            // 设置textureType为Sprite
            textureImporter.textureType = TextureImporterType.Sprite;
            // 重新导入Texture以应用更改
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(textureObj));
        }
    }
}