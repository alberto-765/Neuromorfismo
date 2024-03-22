using System.ComponentModel.DataAnnotations;

namespace Neuromorfismo.BackEnd.Model {
    
    public class BaseModel {
        public DateTime FechaCreac { get; set; }

        [ConcurrencyCheck]
        public DateTime FechaUltMod { get; set; } 
    }
}
