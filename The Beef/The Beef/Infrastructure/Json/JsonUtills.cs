using System.Text.Json;
using System.Text.Json.Serialization;

namespace The_Beef.Infrastructure.Json;

public static class JsonStore
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public static async Task EnsureFileAsync(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        if (!File.Exists(path))
            await File.WriteAllTextAsync(path, "[]");
    }

    public static async Task<List<T>> LoadListAsync<T>(string path)
    {
        await EnsureFileAsync(path);
        
        string json = await File.ReadAllTextAsync(path);
        try
        {
            return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
        }
        catch (JsonException)
        {
            return new List<T>();
        }
    }

    public static async Task SaveListAsync<T>(string path, List<T> items)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        var tmp    = path + ".tmp";
        var backup = path + ".bak";

        var json = JsonSerializer.Serialize(items, Options);
        await File.WriteAllTextAsync(tmp, json);


        try
        {
            if (File.Exists(path))
            {
                File.Replace(tmp, path, backup, ignoreMetadataErrors: true);
                if (File.Exists(backup)) File.Delete(backup);
            }
            else
            {
                File.Move(tmp, path);
            }
        }
        catch (PlatformNotSupportedException)
        {
            File.Copy(tmp, path, overwrite: true);
            File.Delete(tmp);
            if (File.Exists(backup)) File.Delete(backup);
        } 
    }
}