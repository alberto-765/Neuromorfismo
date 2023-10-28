using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IEncriptador {
        string Encriptar (string claveDesencriptada);
        string Desencriptar(string claveEncriptada);
    }
}
