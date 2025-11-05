using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace FiapCloudGames.Application.Tests.Extensions;

[ExcludeFromCodeCoverage]
internal static class DateTimeExtensions
{
    internal static DateTime DataConvertida(string data) =>
        DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
}
