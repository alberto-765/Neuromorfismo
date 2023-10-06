using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface ILoginService {
        Task Login(string token);
        Task Logout();
    }
}
