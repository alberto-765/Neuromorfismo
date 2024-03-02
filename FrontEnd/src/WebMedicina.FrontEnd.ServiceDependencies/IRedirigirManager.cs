namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IRedirigirManager {
        Task RedirigirPagAnt();
        Task RedirigirDefault(string enlace = "/");
    }
}
