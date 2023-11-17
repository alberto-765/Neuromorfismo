using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Service {
    public class ExcepcionPersonalizada : Exception {
        public void ConstruirPintarExcepcion(Exception e) {
            Type tipo = e.GetType();
            string mensaje = e.Message;
            string camino = e.StackTrace ?? string.Empty;

            // Generamos la excepcion y la pintamos por consola 
            String respuesta = $"- Tipo: {tipo}\n- Mensaje: {mensaje}\n- Camino: {camino}";
            Console.Write(respuesta);
        }
    }
}
