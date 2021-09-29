// <copyright file="IGenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data.Interfaces
{
	using System.Linq;

	public interface IGenericRepository<T>
	{
		IQueryable<T> GetAll();

		T Insert(T entity);

		T Update(T entity);

		void Delete(T entity);
	}
}
