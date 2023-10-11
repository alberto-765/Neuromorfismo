using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IRedirigirManager {
        Task ActualizarSeguimientoEnlace();
        Task RedirigirPagAnt();
        Task RedirigirLogin();
        void RedirigirDefault(string enlace = "");
    }
}
