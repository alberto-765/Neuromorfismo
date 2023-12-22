using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IComun {
        string GenerarHtmlErrores(List<ValidationResult> errores);
    }
}
