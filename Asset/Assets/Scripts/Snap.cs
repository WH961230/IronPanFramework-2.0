using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Snap : MonoBehaviour {
    //渲染相机
    [SerializeField] private Camera renderCamera;

    //渲染图片
    [SerializeField] private RenderTexture renderTexture;

    //目标画布
    [SerializeField] private Canvas canvas;

    void Start() {
        renderCamera.targetTexture = renderTexture;
    }

    void Update() {
        //按下 空格键
        if (Input.GetKeyDown(KeyCode.Space)) {
            TakeSnap();
        }
    }

    //截图
    private void TakeSnap() {
        //渲染相机渲染到目标纹理
        RenderTexture.active = renderTexture;
        //创建一个新的纹理
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        //读取像素
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        //应用
        texture2D.Apply();
        //重置
        RenderTexture.active = null;

        //保存
        byte[] bytes = texture2D.EncodeToPNG();
        string pngName = Time.realtimeSinceStartup.ToString();
        string path = Application.dataPath + $"/Snap_{pngName}.png";
        System.IO.File.WriteAllBytes(path, bytes);
        AssetDatabase.Refresh(); //刷新

        //设置为 Sprite
        texture2D = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/Snap_{pngName}.png");
        SetTextureTypeToSprite(texture2D);
        //读取 Sprite
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Snap_{pngName}.png");

        //创建一个新的GameObject
        GameObject imageObj = new GameObject("SnapImage");
        //添加Image组件
        Image image = imageObj.AddComponent<Image>();
        //设置Sprite
        image.sprite = sprite;
        //设置父物体为画布
        image.transform.SetParent(canvas.transform);
        //设置位置
        RectTransform rectTransform = image.GetComponent<RectTransform>();
        //设置中央位置
        rectTransform.localPosition = Vector3.zero;
        //设置大小
        rectTransform.sizeDelta = new Vector2(500, 500);
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