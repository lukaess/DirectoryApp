namespace Business.Services
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using AutoMapper;
	using Business.Interfaces;
	using Data.Entities;
	using Data.Interfaces;
	using Microsoft.EntityFrameworkCore;

	public class EntityService<TEntityType, TDTOType> : IEntityService<TEntityType, TDTOType>
		where TEntityType : BaseEntity
	{
		private readonly IGenericRepository<TEntityType> repository;
		private readonly IMapper mapper;

		public EntityService(IMapper map, IGenericRepository<TEntityType> repository)
		{
			this.mapper = map;
			this.repository = repository;
		}

		public async Task Delete(long id)
		{
			TEntityType entity = await this.GetById(id);
			this.repository.Delete(entity);
			return;
		}

		public async Task<IEnumerable<TDTOType>> GetAll()
		{
			return this.mapper.Map<List<TDTOType>>(this.repository.GetAll());
		}

		public async Task<TEntityType> GetById(long id)
		{
			return await this.repository.GetAll()
				.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<TDTOType> Insert(TDTOType entity)
		{
			var entityDB = this.repository.Insert(this.mapper.Map<TEntityType>(entity));
			return this.mapper.Map<TDTOType>(entityDB);
		}

		public async Task<TDTOType> Update(TDTOType entity, long id)
		{
			TEntityType entityInDB = await this.GetById(id);
			entityInDB = this.mapper.Map(entity, entityInDB);
			this.repository.Update(entityInDB);
			return this.mapper.Map<TDTOType>(entityInDB);
		}
	}
}
