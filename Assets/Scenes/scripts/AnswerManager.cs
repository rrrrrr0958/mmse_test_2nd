using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour
{
    [Header("民國年三位數")]
    public List<InputNumber> YearDigits;

    [Header("月份兩位數")]
    public List<InputNumber> MonthDigits;

    [Header("日期兩位數")]
    public List<InputNumber> DayDigits;

    [Header("星期 0-6")]
    public InputNumber WeekDigit;

    [Header("季節 Toggle Group")]
    public ToggleGroup SeasonToggleGroup;

    public void CheckAnswer()
    {
        int score = 0;

        // 1. 正確答案
        DateTime today = DateTime.Now;
        int rocYear = today.Year - 1911;
        int month = today.Month;
        int day = today.Day;
        int weekday = (int)today.DayOfWeek; // Sunday = 0
        string season = GetSeason(today);

        // 2. 使用者輸入
        int userYear = YearDigits[0].Num * 100 + YearDigits[1].Num * 10 + YearDigits[2].Num;
        int userMonth = MonthDigits[0].Num * 10 + MonthDigits[1].Num;
        int userDay = DayDigits[0].Num * 10 + DayDigits[1].Num;
        int userWeekday = WeekDigit.Num;
        string userSeason = GetSelectedSeason();

        // 3. 比對
        if (userYear == rocYear) score++;
        if (userMonth == month) score++;
        if (userDay == day) score++;
        if (userWeekday == weekday) score++;
        if (userSeason == season) score++;

        Debug.Log($"✅ 得分：{score} / 5");
        Debug.Log($"🧠 正確答案：{rocYear}年 {month}月 {day}日 星期{GetChineseWeekday(weekday)} {season}");
        Debug.Log($"📝 你的答案：{userYear}年 {userMonth}月 {userDay}日 星期{GetChineseWeekday(userWeekday)} {userSeason}");
    }

    private string GetSelectedSeason()
    {
        foreach (var toggle in SeasonToggleGroup.ActiveToggles())
        {
            return toggle.GetComponentInChildren<TextMeshProUGUI>().text;
        }
        return "";
    }

    private string GetSeason(DateTime date)
    {
        int m = date.Month;
        if (m >= 3 && m <= 5) return "春";
        if (m >= 6 && m <= 8) return "夏";
        if (m >= 9 && m <= 11) return "秋";
        return "冬";
    }

    private string GetChineseWeekday(int weekday)
    {
        string[] days = { "日", "一", "二", "三", "四", "五", "六" };
        return days[weekday % 7];
    }
}
