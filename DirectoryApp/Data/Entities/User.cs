// <copyright file="User.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class User : BaseEntity
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Surname { get; set; }

		public string Adress { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public DateTime DateOfBirth { get; set; }

		public bool IsAdmin { get; set; }
	}
}
