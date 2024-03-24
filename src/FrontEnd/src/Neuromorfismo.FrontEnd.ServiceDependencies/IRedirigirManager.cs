namespace Neuromorfismo.FrontEnd.ServiceDependencies {
    public interface IRedirigirManager {

        /// <summary>
        /// Obtenemos la url de segumiento y redirigimos 
        /// </summary>
        /// <returns></returns>
        Task RedirigirPagAnt();

        /// <summary>
        /// Redirige a la url especificada o al inicio
        /// </summary>
        /// <param name="enlace"></param>
        /// <returns></returns>
        Task RedirigirDefault(string enlace = "/");

        /// <summary>
        /// Actualiza la url de segumiento
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task ActualizarSeguimiento(string url);
    }
}
