using System.Globalization;
using System.Text;

namespace Neuromorfismo.BackEnd.ServicesDependencies.ExtensionMethods
{
    public static class ExtensionsMethods
    {
        public static string SinTildes(this string texto) =>
            new string(
                texto.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray()
            ).Normalize(NormalizationForm.FormC);
    }
}
