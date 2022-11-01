using Newtonsoft.Json;
using Rtl.TvMazeScrapper.Domain.DTO.Base;
using Rtl.TvMazeScrapper.Domain.Extensions;

namespace Rtl.TvMazeScrapper.Infrastructure.Extensions;

public static class JsonExtensions
{
    public static BaseDTO<T> TryDeserialize<T>(this string json)
    {
        var result = new BaseDTO<T>();

        try
        {
            result.Data = JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception ex)
        {
            result.AddError($"Failed to deserialize json to type of {typeof(T)} - {ex.Message()}");
        }

        return result;
    }
}