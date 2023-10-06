using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.Service {
    public class AuthenticationStateProviderFalso : AuthenticationStateProvider {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync() {
            var identity = new ClaimsIdentity(); 
            //var identity = new ClaimsIdentity(new List<Claim> {
            //    new Claim(ClaimTypes.Name, "Felipe")
            //}, "prueba");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
    }
}
