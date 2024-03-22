namespace Neuromorfismo.BackEnd.ServicesDependencies {
    public interface IEmailService {
        /// <summary>
        ///  Enviamos mensaje de correo
        /// </summary>
        /// <param name="asunto"></param>
        /// <param name="mensaje"></param>
        /// <param name="imagen"></param>
        void Send(string asunto, string mensaje, MemoryStream imagen);
    }
}
