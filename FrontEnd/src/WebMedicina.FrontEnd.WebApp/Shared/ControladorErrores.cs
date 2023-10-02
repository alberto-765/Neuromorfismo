using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace WebMedicina.FrontEnd.WebApp.Shared {
	public class ControladorErrores : ErrorBoundary{
		[Inject]
		private IWebAssemblyHostEnvironment entorno { get; set; }


		protected override Task OnErrorAsync(Exception exception) {
			if(entorno.IsProduction()) 
				return Task.CompletedTask;
			else
				return base.OnErrorAsync(exception);	
		}
	}
}
