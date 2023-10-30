using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class EpilepsiasDto {
        public int IdEpilepsia { get; set; }

        public string Nombre { get; set; } =string.Empty;

        public DateOnly FechaCreac { get; set; }

        public DateOnly FechaUltMod { get; set; }
    }
}
