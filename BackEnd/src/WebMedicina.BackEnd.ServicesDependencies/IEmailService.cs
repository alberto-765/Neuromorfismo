namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IEmailService {
        void Send(string asunto, string mensaje, MemoryStream imagen);
    }
}
