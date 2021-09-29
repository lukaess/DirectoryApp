// <copyright file="BaseEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data.Entities
{
	using System.ComponentModel.DataAnnotations;
	using Data.Interfaces;

	public class BaseEntity : IBaseEntity
	{
		[Key]
		public long Id { get; set; }
	}
}
