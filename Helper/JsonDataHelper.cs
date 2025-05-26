using ChequePrintWebApp.Models;
using System.Text.Json;



public static class JsonDataHelper
{
    private static string _filePath = Path.Combine("App_Data", "history.json");

    public static List<ChequeHistory> LoadHistory()
    {
        if (!File.Exists(_filePath))
            return new List<ChequeHistory>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<ChequeHistory>>(json) ?? new List<ChequeHistory>();
    }

    public static void SaveHistory(List<ChequeHistory> history)
    {
        var json = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });

        var dir = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir!); // create folder if missing

        File.WriteAllText(_filePath, json);
    }
}
