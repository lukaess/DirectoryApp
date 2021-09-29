// <copyright file="ModelBuilderExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data.Extensions
{
	using System;
	using Data.Entities;
	using Microsoft.EntityFrameworkCore;

	public static class ModelBuilderExtension
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				new User
				{
					Id = 1,
					Name = "Lukas",
					Surname = "Krištić",
					Email = "lkristic@eko.hr",
					Adress = "Slavonski Brod, Branimirova 86",
					DateOfBirth = DateTime.Parse("24.10.1995"),
					Password = "Kong35",
					IsAdmin = true,
				},
				new User
				{
					Id = 2,
					Name = "Mato",
					Surname = "Krištić",
					Email = "mato@eko.hr",
					Adress = "Slavonski Brod, Branimirova 86",
					DateOfBirth = DateTime.Parse("10.10.1963"),
					Password = "Baki49",
					IsAdmin = true,
				},
				new User
				{
					Id = 3,
					Name = "Tony",
					Surname = "Mitrandil",
					Email = "tmintra@eko.hr",
					Adress = "Osijek, Stjepana Radića 2",
					DateOfBirth = DateTime.Parse("17.09.1991"),
					Password = "Legolas4",
					IsAdmin = false,
				},
				new User
				{
					Id = 4,
					Name = "Artemida",
					Surname = "Olimp",
					Email = "aolimp@eko.hr",
					Adress = "Rijeka, Ivana Držića 7",
					DateOfBirth = DateTime.Parse("11.05.1993"),
					Password = "Hades191",
					IsAdmin = false,
				});
		}
	}
}
