// <copyright file="UserDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Business.Views
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class UserDTO
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Surname { get; set; }

		public string Adress { get; set; }

		[Required]
		public string Email { get; set; }

		public DateTime DateOfBirth { get; set; }
	}
}
