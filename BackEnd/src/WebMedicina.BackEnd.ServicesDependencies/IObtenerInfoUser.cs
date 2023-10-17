using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IObtenerInfoUser {
        UserInfoDto ConvervirToken(ClaimsPrincipal User);
    }
}
