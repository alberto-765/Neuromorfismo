using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IComun {
        string GenerarHtmlErrores(List<ValidationResult> errores);
        Task BloquearScroll(string idDialogo, string eje);
        Task BloquearScroll(string idDialogo);
        Task DesbloquearScroll(string idDialogo, string eje);
        Task DesbloquearScroll(string idDialogo);
        Task ScrollHaciaElemento(string idElemento, string posY = "center");
        Task ScrollBottom();
    }
}
