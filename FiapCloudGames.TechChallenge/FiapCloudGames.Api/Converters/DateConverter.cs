using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FiapCloudGames.Api.Converters;

/// <summary>
/// Converte datas durante a serialização e desserialização JSON.
/// </summary>
/// <remarks>Suporta DateTime e DateTime? (nullable).</remarks>
[ExcludeFromCodeCoverage]
public sealed class DateConverter : JsonConverter<DateTime?>
{
    private const string BrDateFormat = "dd/MM/yyyy";
    private static readonly CultureInfo PtBrCulture = new("pt-BR");

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        var value = reader.GetString();

        if (string.IsNullOrWhiteSpace(value))
            return null;

        if (DateTime.TryParseExact(value, BrDateFormat, PtBrCulture, DateTimeStyles.None, out var date))
            return date;

        if (DateTime.TryParse(value, PtBrCulture, DateTimeStyles.None, out date))
            return date;

        throw new JsonException($"Formato de data inválido. Use o formato {BrDateFormat}.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(BrDateFormat, PtBrCulture));
        else
            writer.WriteNullValue();
    }
}