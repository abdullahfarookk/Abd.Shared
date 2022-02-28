using Abd.Shared.Core.Environments;

namespace Abd.Shared.Core.JsonUtils;

public static class FileReader
{
    private static string _testProject;
    private static string _currentProject;
    public static void SetPath(string testProject, string currentProject)
    {
        _testProject = testProject;
        _currentProject = currentProject;
    }
    public static List<T> GetDataFromJson<T>(string pathToFile, Func<IEnumerable<T>, IEnumerable<T>>? select = null)
    {
        if (!pathToFile.Contains("json")) 
            throw new Exception("File format not supported");

        var data = new List<T>();

        var path = GetPath(pathToFile);

        using var stream = new StreamReader(path);
        var json = stream.ReadToEnd();
        var objects = json.ToObject<List<T>>();
        data.AddRange(@select is null ? objects : @select(objects));


        return data;
    }
    private static string GetPath(string fileName)
    {
        var path = Environment.CurrentDirectory;
        return Path.Combine(
            ApplicationEnvironment.IsTest
                ? $"{path.Substring(0, path.IndexOf(_testProject?? throw new Exception("Test Project Name is not Defined"), StringComparison.Ordinal))}{_currentProject?? throw new Exception("Current Project Name is not defined")}"
                : path, @"wwwroot/assets", fileName);
    }
}