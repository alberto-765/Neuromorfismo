using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class LLamadaUploadUser {
        public UserUploadDto usuario { get; set; } = new();
        public bool rolModificado { get; set; }
    }
}
