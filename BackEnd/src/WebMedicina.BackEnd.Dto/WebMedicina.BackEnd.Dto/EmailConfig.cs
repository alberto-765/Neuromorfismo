
using System.Collections.Immutable;

namespace WebMedicina.BackEnd.Dto {
    public class EmailConfig {
        public string Usuario { get; set; } = default!;
        public string Contrasena { get; set; } = default!;
        public string Host { get; set; } = default!;
        public int Puerto { get; set; }
        public bool Ssl { get; set; } 
        public bool DefaultCredencials { get; set; } 
        public ImmutableArray<string> Destinatarios { get; set; }
    }
}
