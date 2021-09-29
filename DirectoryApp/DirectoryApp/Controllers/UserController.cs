// <copyright file="UserController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DirectoryApp.Controllers
{
	using System;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Business.Interfaces;
	using Business.Models;
	using Business.Views;
	using Data.Entities;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IEntityService<User, NewUserDTO> userGenericService;
		private readonly IUserService userService;
		private readonly IJwtAuthService jwtAuthService;

		public UserController(IEntityService<User, NewUserDTO> userRepository, IUserService userService, IJwtAuthService jwtAuthService)
		{
			this.userGenericService = userRepository;
			this.userService = userService;
			this.jwtAuthService = jwtAuthService;
		}

		[HttpGet]
		public async Task<IActionResult> GetStudents()
		{
			var studnents = await this.userGenericService.GetAll();
			return this.Ok(studnents);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<NewUserDTO>> GetStudent(long id)
		{
			var student = await this.userGenericService.GetById(id);
			return this.Ok(student);
		}

		[HttpPost]
		public async Task<ActionResult<NewUserDTO>> AddStudent([FromBody] NewUserDTO user)
		{
			if (await this.userService.CheckUserEmail(user.Email))
			{
				this.ModelState.AddModelError("Existing mail", "The user email already exists!");
				return BadRequest(this.ModelState);
			}
			var newUser = await this.userGenericService.Insert(user);
			return this.Ok(newUser);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<NewUserDTO>> DeleteStudent(long id)
		{
			await this.userGenericService.Delete(id);
			return this.Ok();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<NewUserDTO>> UpdateStudent(long id, NewUserDTO user)
		{
			var result = await this.userGenericService.Update(user, id);
			if (result == null)
			{
				return this.NotFound();
			}

			return this.Ok();
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] LogInUserDTO request)
		{
			var user = await this.userService.GetUser(request);

			if (user == null || !user.IsAdmin)
			{
				return this.Unauthorized();
			}

			var claims = new[]
			{
				new Claim(ClaimTypes.Email, request.Email),
				new Claim(ClaimTypes.Name, user.Name),
			};

			var jwtResult = this.jwtAuthService.GenerateTokens(request.Email, claims, DateTime.Now);

			return this.Ok(new LogInResult
			{
				UserName = user.Name,
				Role = "Admin",
				Email = user.Email,
				AccessToken = jwtResult.AccessToken,
				RefreshToken = jwtResult.RefreshToken.TokenString,
			});
		}
	}
}
