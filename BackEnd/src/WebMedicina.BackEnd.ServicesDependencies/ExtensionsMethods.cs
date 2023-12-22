using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public static class ExtensionsMethods {
            public static string SinTildes(this string texto) =>
                new String(
                    texto.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    .ToArray()
                ).Normalize(NormalizationForm.FormC);
    }
}
