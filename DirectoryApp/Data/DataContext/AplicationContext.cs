// <copyright file="AplicationContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data.DataContext
{
	using Data.Entities;
	using Data.Extensions;
	using Microsoft.EntityFrameworkCore;

	public class AplicationContext : DbContext
	{
		public AplicationContext(DbContextOptions<AplicationContext> options)
            : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>();
			modelBuilder.Seed();
		}
	}
}
