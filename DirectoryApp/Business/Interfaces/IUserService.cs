namespace Business.Interfaces
{
    using System.Threading.Tasks;
    using Business.Views;

    public interface IUserService
    {
        Task<NewUserDTO> GetUser(LogInUserDTO loginUser);

        Task<bool> CheckUserEmail(string email);
    }
}
