using MudBlazor;


namespace Neuromorfismo.FrontEnd.Service {
    public class EstilosBase {
        public MudTheme currentTheme = new() {
            Palette = new PaletteLight {
                Primary = "#64B5F6",
                Secondary = "#1565C0",
                AppbarBackground = "#64B5F6",
                DrawerBackground = "#64B5F6",
                AppbarText = "#424242",
                DrawerText = "#424242",
                DrawerIcon = "#424242"
            },
            PaletteDark = new PaletteDark {
                Primary = "#64B5F6",
                Secondary = "#42A5F5",
                Success = "#2ECC40",
                AppbarBackground = "#1D1D1D",
                DrawerBackground = "#1D1D1D",
                DrawerText = "#FAFAFA",
                DrawerIcon = "#FAFAFA",
                AppbarText = "#FAFAFA",
                Dark = "#EEEEEE",
                White = "#000000",
                TextPrimary = "#ececf1",
                //Background = "#232323",
                //Surface = "#2B2B2B"
            }    
        };
    }
}
