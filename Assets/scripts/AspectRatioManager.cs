using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    void Start()
    {
        // 9:16 aspect ratio sabitleme
        float targetAspect = 9f / 16f; // 9:16 oranı

        // Şu anki ekranın aspect ratio'su
        float windowAspect = (float)Screen.width / (float)Screen.height;
        // Yeni çözünürlük ayarı
        float scaleHeight = windowAspect / targetAspect;
        Camera camera = Camera.main;

        // Ekran oranını ayarla
        if (scaleHeight < 1.0f)
        {
            // Yükseklik oranını küçült
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            // Genişlik oranını küçült
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }

        // Ekran çözünürlüğünü sabitle
        Screen.SetResolution(1080, 1920, false); // 1080x1920 çözünürlüğü (9:16)
    }
}
