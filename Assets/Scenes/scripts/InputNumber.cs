using UnityEngine;
using TMPro;

public class InputNumber : MonoBehaviour
{
    public enum InputMode
    {
        Number,
        Weekday
    }

    [Header("顯示模式")]
    public InputMode inputMode = InputMode.Number;

    [Header("最大值")]
    public int maxNum = 9;

    [Header("最小值")]
    public int minNum = 0;

    [Header("步長")]
    public int step = 1;

    [Header("顯示用 TextMeshPro")]
    public TextMeshProUGUI displayText;

    [SerializeField, Header("目前值")]
    private int num = 0;

    private readonly string[] weekdays = { "一", "二", "三", "四", "五", "六", "日" };

    void Start()
    {
        UpdateDisplay();
    }

    public void Increase()
    {
        num += step;
        if (num > maxNum) num = maxNum;
        UpdateDisplay();
    }

    public void Decrease()
    {
        num -= step;
        if (num < minNum) num = minNum;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            if (inputMode == InputMode.Weekday)
            {
                // 安全檢查：避免超出範圍
                int index = Mathf.Clamp(num, 0, weekdays.Length - 1);
                displayText.text = weekdays[index];
            }
            else
            {
                displayText.text = num.ToString();
            }
        }
    }

    // 若你要取出目前選擇的星期或數字（比對用）
    public int GetValue()
    {
        return num;
    }

    public string GetWeekday()
    {
        if (inputMode == InputMode.Weekday)
        {
            int index = Mathf.Clamp(num, 0, weekdays.Length - 1);
            return weekdays[index];
        }
        return "";
    }
}
