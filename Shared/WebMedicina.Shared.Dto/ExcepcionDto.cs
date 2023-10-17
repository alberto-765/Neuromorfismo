using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class ExcepcionDto : Exception{
        private Type Tipo; // tipo de la excepcion
        private string Mensaje; // mensaje de la excepcion
        private string Camino; // camino de localizacion

        public void ConstruirPintarExcepcion(Exception e) {
            Tipo = e.GetType();
            Mensaje = e.Message;
            Camino = e.StackTrace;

            // Generamos la excepcion y la pintamos por consola 
            String respuesta = $"- Tipo: {Tipo}\n- Mensaje: {Mensaje}\n- Camino: {Camino}";
            Console.Write(respuesta);
        }
    }
}
