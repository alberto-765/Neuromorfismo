
namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface ILoginService {
        Task Login(string token);
        Task Logout();
    }
}
