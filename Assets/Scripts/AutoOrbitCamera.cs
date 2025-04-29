using UnityEngine;

public class AutoOrbitCamera : MonoBehaviour
{
    public Transform target;               // 要環繞的目標
    public float distance = 5.0f;          // 與目標的距離
    public float orbitSpeed = 20.0f;       // 自動旋轉速度（度/秒）
    public float verticalAngle = 20.0f;    // 仰角（0 = 水平，90 = 垂直往下看）
    public Vector3 offset = Vector3.up;    // 鏡頭注視點的偏移（例如 LOGO 高度）

    private float currentAngle = 0.0f;

    void Update()
    {
        if (target == null) return;

        // 角度轉為弧度
        currentAngle += orbitSpeed * Time.deltaTime;
        float horizontalRadians = currentAngle * Mathf.Deg2Rad;
        float verticalRadians = verticalAngle * Mathf.Deg2Rad;

        // 使用球坐標計算攝影機位置
        float x = Mathf.Cos(verticalRadians) * Mathf.Sin(horizontalRadians) * distance;
        float y = Mathf.Sin(verticalRadians) * distance;
        float z = Mathf.Cos(verticalRadians) * Mathf.Cos(horizontalRadians) * distance;

        Vector3 orbitPosition = new Vector3(x, y, z);
        Vector3 targetWithOffset = target.position + offset;

        transform.position = targetWithOffset + orbitPosition;
        transform.LookAt(targetWithOffset); // 永遠看著偏移後的位置
    }
}
