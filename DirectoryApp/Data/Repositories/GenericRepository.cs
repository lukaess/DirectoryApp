// <copyright file="GenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data.Repositories
{
	using System;
	using System.Linq;
	using Data.DataContext;
	using Data.Interfaces;
	using Microsoft.EntityFrameworkCore;

	public class GenericRepository<T> : IGenericRepository<T>
	    where T : class
	{
		protected readonly AplicationContext appContext;

		private DbSet<T> Entities { get; set; }

		public GenericRepository(AplicationContext appContext)
		{
			this.appContext = appContext;
			this.Entities = appContext.Set<T>();
		}

		public void Delete(T entity)
		{
			this.Entities.Remove(entity);
			this.appContext.SaveChanges();
		}

		public IQueryable<T> GetAll()
		{
			return this.Entities;
		}

		public T Insert(T entity)
		{
			this.Entities.Add(entity);
			this.appContext.SaveChanges();
			return entity;
		}

		public T Update(T entity)
		{
			this.appContext.SaveChanges();
			return entity;
		}
	}
}
