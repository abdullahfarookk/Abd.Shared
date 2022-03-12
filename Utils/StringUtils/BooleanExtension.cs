namespace Abd.Shared.Utils.StringUtils;
public class BooleanExtension
{
    public static bool IsEitherTrue(bool firstStatement, bool secondStatement) => firstStatement ^ secondStatement;
    public static bool IsEitherTrue(bool firstStatement, bool secondStatement, bool thirdStatement) => firstStatement ^ secondStatement ^ thirdStatement;
    public static bool IsEitherTrue(List<bool> statements) => statements.Any() && statements.First() ^ statements.Skip(1).Aggregate((st1, st2) => st1 || st2);
}
