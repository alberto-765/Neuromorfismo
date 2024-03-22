using Neuromorfismo.Shared.Dto.UserAccount;

namespace Neuromorfismo.FrontEnd.ServiceDependencies {
    public interface ILoginService {
        Task Login(Tokens tokens);
        Task Logout();
    }
}
