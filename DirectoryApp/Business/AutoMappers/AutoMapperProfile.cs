namespace Business.Automappers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Business.Views;
    using Data.Entities;

    public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			this.CreateMap<User, User>();
			this.CreateMap<List<User>, List<UserDTO>>();
			this.CreateMap<User, NewUserDTO>();
			this.CreateMap<NewUserDTO, User>();
			this.CreateMap<LogInUserDTO, User>();
		}
	}
}
