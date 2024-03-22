using System.ComponentModel.DataAnnotations;

namespace Neuromorfismo.FrontEnd.ServiceDependencies {
    public interface IComun {

        /// <summary>
        /// Mostrar lista con los errores encontrados
        /// </summary>
        /// <param name="errores"></param>
        /// <returns></returns>
        string GenerarHtmlErrores(List<ValidationResult> errores);

        /// <summary>
        /// Quitamos scroll del dialogo por medio de js
        /// </summary>
        /// <param name="idDialogo"></param>
        /// <param name="eje"></param>
        /// <returns></returns>
        Task BloquearScroll(string idDialogo, string eje);
        /// <summary>
        /// Quitamos scroll del dialogo por medio de js
        /// </summary>
        /// <param name="idDialogo"></param>
        /// <returns></returns>
        Task BloquearScroll(string idDialogo);

        Task DesbloquearScroll(string idDialogo, string eje);

        /// <summary>
        /// Devolvemos scroll del dialogo por medio de js
        /// </summary>
        /// <param name="idDialogo"></param>
        /// <param name="eje"></param>
        /// <returns></returns>
        Task DesbloquearScroll(string idDialogo);

        /// <summary>
        /// Hacer scroll a una posicion de un elemento por el id
        /// </summary>
        /// <param name="selectorElemento"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        Task ScrollHaciaElemento(string selectorElemento, string posY = "center");

        /// <summary>
        /// Hacer scroll al bottom de la pantalla
        /// </summary>
        /// <returns></returns>
        Task ScrollBottom();
        Task FadeIn(string selectorElemento, ushort duracion = 500);
        Task FadeOut(string selectorElemento, ushort duracion = 500);
    }
}
