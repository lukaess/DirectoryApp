// <copyright file="IBaseEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data.Interfaces
{
    using System.ComponentModel.DataAnnotations;

    public interface IBaseEntity
	{
		public long Id { get; set; }
	}
}
