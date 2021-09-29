namespace Business.Interfaces
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IEntityService<TEntityType, TDTOType>
	{
		public Task<IEnumerable<TDTOType>> GetAll();

		public Task<TEntityType> GetById(long id);

		public Task<TDTOType> Insert(TDTOType entity);

		public Task<TDTOType> Update(TDTOType entity, long id);

		public Task Delete(long id);
	}
}