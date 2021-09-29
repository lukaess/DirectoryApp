// <copyright file="LogInUserDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Business.Views
{
	using System.ComponentModel.DataAnnotations;

	public class LogInUserDTO
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
