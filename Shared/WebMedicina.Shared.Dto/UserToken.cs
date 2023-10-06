using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class UserToken {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
