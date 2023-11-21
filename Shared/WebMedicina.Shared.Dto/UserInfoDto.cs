using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class UserInfoDto {
        public int IdMedico { get; set; }
        public string UserLogin { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNac { get; set; } 
        public DateOnly FechaCreac { get; set; }
        public DateOnly FechaUltMod { get; set; }
        public string Rol { get; set; }
        public string Sexo { get; set; }
    }
}
