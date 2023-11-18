using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class CrearPaciente {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private void Submit() {
            MudDialog.Close(DialogResult.Ok(true));
        }

        private void Cancel() => MudDialog.Cancel();
    }
}
