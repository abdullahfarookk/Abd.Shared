namespace Abd.Shared.Utils.DateTimeUtils;

public static class DateTimeExtension
{
    public static DateTime ToDateTime(this DateTime? value)
        => Convert.ToDateTime(value);
    public static List<DateTime> GetWeek(this DateTime date)
    {
        DateTime today = date;
        int currentDayOfWeek = (int)today.DayOfWeek;
        DateTime sunday = today.AddDays(-currentDayOfWeek);
        DateTime monday = sunday.AddDays(1);
        // If we started on Sunday, we should actually have gone *back*
        // 6 days instead of forward 1...
        if (currentDayOfWeek == 0)
        {
            monday = monday.AddDays(-7);
        }
        var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
        return dates;
    }
}