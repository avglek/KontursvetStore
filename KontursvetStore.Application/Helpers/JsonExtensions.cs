using System.Text.Json;

namespace KontursvetStore.Application.Helpers;

public static class JsonExtensions
{
    /// <summary>
    /// Converts an object to its JSON string representation.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>A JSON string representing the object.</returns>
    public static string ToJson(this object obj)
    {
        // You can customize serialization options here, e.g., for pretty printing
        // var options = new JsonSerializerOptions { WriteIndented = true };
        // return JsonSerializer.Serialize(obj, options);
        return JsonSerializer.Serialize(obj);
    }
}