using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.Service {
    public class EstilosBase {
        public MudTheme currentTheme = new() {
            Palette = new PaletteLight {
                Primary = "#64B5F6",
                Secondary = "#1565C0",
                // Info = "#64a7e2",
                // Success = "#2ECC40",
                // Warning = "#FFC107",
                // Error = "#FF0000",
                AppbarBackground = "#64B5F6",
                DrawerBackground = "#64B5F6",
                AppbarText = "#424242",
                DrawerText = "#424242",
                DrawerIcon = "#424242",
            },
            PaletteDark = new PaletteDark {

                Primary = "#64B5F6",
                Secondary = "#42A5F5",
                // Info = "#a4c2dd",
                // Success = "#2ECC40",
                // Warning = "#dc2d7e",
                // Error = "#de2000",
                AppbarBackground = "rgba(26,26,39,0.8)",
                DrawerBackground = "rgba(26,26,39,0.8)",
                DrawerText = "#FAFAFA",
                DrawerIcon = "#FAFAFA",
                AppbarText = "#FAFAFA",
                
                // TextPrimary = "#FFFFFF",
                // TextSecondary = "#BDBDBD",
                // DarkContrastText = "#FFFFFF",
                Dark = "#EEEEEE",
                White = "#000000",
                TextPrimary= "#ececf1",
            }
        };

        // Mostrar overlay de carga
        public bool _mostrarCargando { get; set; }
    }
}
