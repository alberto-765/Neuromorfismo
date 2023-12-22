using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.Service {
    public class Comun : IComun {
        public string GenerarHtmlErrores(List<ValidationResult> errores) {
            try {
                string listaErrores = "<p><b>Algunos campos no son correctos y no es posible editar el usuario:</b></p><ol style='margin-left: 15px; margin-top: 10px;'>";

                foreach (ValidationResult validacion in errores) {
                    listaErrores += $"<li>{validacion.ErrorMessage}</li>";
                }
                listaErrores += "</ol>";

                return listaErrores;
            } catch (Exception) {
                throw;
            }
        }

    }
}
