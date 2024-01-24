using WebMedicina.Shared.Dto.UserAccount;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface ILoginService {
        Task Login(Tokens tokens);
        Task Logout();
    }
}
