using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GazeTriggerUI : MonoBehaviour
{
    public Camera mainCamera;           // 開場環繞鏡頭
    public Camera targetCamera;         // 切換後的遊戲鏡頭
    public GameObject blurVolumeObject; // Volume 組件 (含 DOF)
    public Canvas logoCanvas;           // LOGO 與提示 Canvas

    private Volume blurVolumeComponent;
    private DepthOfField dof;
    private bool hasTriggered = false;

    void Start()
{
    if (mainCamera != null) mainCamera.enabled = true;
    if (targetCamera != null) targetCamera.enabled = false;

    // 確保 DOF 有正確參考
    if (blurVolumeObject != null)
    {
        blurVolumeComponent = blurVolumeObject.GetComponent<Volume>();
        if (blurVolumeComponent != null)
        {
            blurVolumeComponent.profile.TryGet(out dof);
        }
    }
}

    void Update()
    {
        if (hasTriggered) return;

        // ✅ 偵測按任意鍵（含鍵盤與VR控制器）
        if (Input.anyKeyDown)
        {
            TriggerAction();
            hasTriggered = true; // 避免重複觸發
        }
    }

    public void TriggerAction()
    {
        // ✅ 鏡頭切換
        if (mainCamera != null) mainCamera.enabled = false;
        if (targetCamera != null) targetCamera.enabled = true;

        // ✅ 關閉模糊
        if (blurVolumeObject != null) blurVolumeObject.SetActive(false);
        if (dof != null) dof.active = false;

        // ✅ 關閉 LOGO UI
        if (logoCanvas != null) logoCanvas.enabled = false;
    }
}
