using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public Camera targetCamera; 
    // Start is called before the first frame update
    void Start()
    {
        targetCamera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    

    public void TakeScreenshot(string name)
    {
        // 创建一个RenderTexture作为截图的目标
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        // 设置相机的目标渲染纹理
        targetCamera.targetTexture = renderTexture;

        // 手动渲染一帧
        targetCamera.Render();

        // 激活目标渲染纹理，并将其内容读取为一张纹理
        RenderTexture.active = renderTexture;
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // 重置相机的目标渲染纹理和激活的渲染纹理
        targetCamera.targetTexture = null;
        RenderTexture.active = null;

        // 销毁中间使用的RenderTexture
        Destroy(renderTexture);

        // 保存截图为文件（可选）
        byte[] bytes = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes(name + ".png", bytes);

        // 在控制台打印截图的尺寸信息（可选）
        Debug.Log("Screenshot size: " + screenshot.width + "x" + screenshot.height);
    }
}
