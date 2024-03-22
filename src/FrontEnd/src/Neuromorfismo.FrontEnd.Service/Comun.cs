using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Neuromorfismo.FrontEnd.ServiceDependencies;

namespace Neuromorfismo.FrontEnd.Service {
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
            await _js.InvokeVoidAsync("desbloquearScroll", idDialogo, "y");
        }
        public async Task DesbloquearScroll(string idDialogo, string eje) {
            await _js.InvokeVoidAsync("desbloquearScroll", idDialogo, eje);
        }


        /// <summary>
        /// Hacer scroll a una posicion de un elemento por el id
        /// </summary>
        /// <param name="idElemento"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        public async Task ScrollHaciaElemento(string selectorElemento, string posY = "center") {
            await _js.InvokeVoidAsync("ScrollHaciaElemento", selectorElemento, posY);   
        }


        /// <summary>
        /// Hacer scroll al bottom de la pantalla
        /// </summary>
        /// <returns></returns>
        public async Task ScrollBottom() {
            await _js.InvokeVoidAsync("ScrollBottom");
        }

        public async Task FadeIn(string selectorElemento, ushort duracion = 500) {
            if (!string.IsNullOrWhiteSpace(selectorElemento)) {
                await _js.InvokeAsync<bool>("FadeIn", selectorElemento, duracion);
            }
        }

        public async Task FadeOut(string selectorElemento, ushort duracion = 500) {
            if (!string.IsNullOrWhiteSpace(selectorElemento)) {
                await _js.InvokeAsync<bool>("FadeOut", selectorElemento, duracion);
            }
        }
    }
}
