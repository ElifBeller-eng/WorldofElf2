using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AspectRatioManager : MonoBehaviour
{
    public float targetAspectRatio = 9f / 16f;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspectRatio;

        if (scaleHeight < 1.0f)
        {
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;
        }
    }
    void Update()
    {
        Debug.Log($"Build Resolution: {Screen.width}x{Screen.height}");
    }
}
