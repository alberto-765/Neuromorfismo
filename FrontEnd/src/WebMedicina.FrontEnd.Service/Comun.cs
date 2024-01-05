using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.Service {
    public class Comun : IComun {
        private readonly IJSRuntime _js;
        public Comun(IJSRuntime js) { 
            _js = js;
        }


        /// <summary>
        /// Mostrar lista con los errores encontrados
        /// </summary>
        /// <param name="errores"></param>
        /// <returns></returns>
        public string GenerarHtmlErrores(List<ValidationResult> errores) {
            try {
                string listaErrores = "<p><b>Algunos de los campos no son correctos:</b></p><ol style='margin-left: 15px; margin-top: 10px;'>";

                foreach (ValidationResult validacion in errores) {
                    listaErrores += $"<li>{validacion.ErrorMessage}</li>";
                }
                listaErrores += "</ol>";

                return listaErrores;
            } catch (Exception) {
                throw;
            }
        }


        /// <summary>
        /// Quitamos scroll del dialogo por medio de js
        /// </summary>
        /// <param name="idDialogo"></param>
        /// <param name="eje"></param>
        /// <returns></returns>
        public async Task BloquearScroll(string idDialogo, string eje) {
            try {
                await _js.InvokeVoidAsync("bloquearScroll", idDialogo, eje);
            } catch (Exception) {
                throw;
            }
        }
        public async Task BloquearScroll(string idDialogo) {
            try {
                await _js.InvokeVoidAsync("bloquearScroll", idDialogo, "y");
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Devolvemos scroll del dialogo por medio de js
        /// </summary>
        /// <param name="idDialogo"></param>
        /// <param name="eje"></param>
        /// <returns></returns>
        public async Task DesbloquearScroll(string idDialogo) {
            try {
                await _js.InvokeVoidAsync("desbloquearScroll", idDialogo, "y");
            } catch (Exception) {
                throw;
            }
        }
        public async Task DesbloquearScroll(string idDialogo, string eje) {
            try {
                await _js.InvokeVoidAsync("desbloquearScroll", idDialogo, eje);
            } catch (Exception) {
                throw;
            }
        }
    }
}
