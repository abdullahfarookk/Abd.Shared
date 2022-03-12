namespace Abd.Shared.Utils.StringUtils;

public static class FirstCharactersOfNWord
{
    public static string FirstCharactersOfNWords(this string value, int length=1)
    {
        var words = value
            .Split(new string[] { " " }, StringSplitOptions.None)
            .Select(x => x.FirstOrDefault())
            .ToList()
            .Take(length);

        return words.Aggregate("", (current, c) => current + c);
    }
    public static string FirstWord(this string value)
        => value.Split(new string[] { " " }, StringSplitOptions.None)[0];

}