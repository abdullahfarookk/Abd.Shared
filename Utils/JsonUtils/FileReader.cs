namespace Abd.Shared.Utils.JsonUtils;

public static class FileReader
{
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
    private static string GetPath(string fileName, string subPath = "wwwroot")
    {
        var path = Environment.CurrentDirectory;
        return Path.Combine(path, subPath, fileName);
    }
}