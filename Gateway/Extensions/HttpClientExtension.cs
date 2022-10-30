using System.Text.Json;

namespace Gateway.Extensions;

public static class HttpClientExtension
{
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonSerializer.Deserialize<T>(dataAsString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
