using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class UserInfo {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string NumHistoria { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaUltMod { get; set; }
        public string Password { get; set; }
    }
}
