namespace Business.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Business.Interfaces;
    using Business.Views;
    using Data.Entities;
    using Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IMapper mapper;

        public UserService(IMapper map, IGenericRepository<User> userRepository)
        {
            this.mapper = map;
            this.userRepository = userRepository;
        }

        public async Task<bool> CheckUserEmail(string email)
		{
            var user = await this.userRepository.GetAll()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
            if (user != null)
			{
                return false;
			}

            return true;
        }

        public async Task<NewUserDTO> GetUser(LogInUserDTO loginUser)
        {
            var user = await this.userRepository.GetAll()
                .Where(u => u.Email == loginUser.Email && u.Password == loginUser.Password)
                .FirstOrDefaultAsync();

            return this.mapper.Map<NewUserDTO>(user);
        }
    }
}
