using InventoryManagement.DTOs;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class AuthController : ControllerBase
	{
		private readonly TokenService _tokenService;

		public AuthController(TokenService tokenService)
		{
			_tokenService = tokenService;
		}

		// Hardcoded users
		private readonly List<(string Username, string Password, string Role)> Users =
			new()
			{
			("manager", "manager@123", "WarehouseManager"),
			("operator", "operator@123", "WarehouseOperator")
			};

		[HttpPost]
		public IActionResult Login([FromBody] LoginRequestDTO model)
		{
			var user = Users.FirstOrDefault(u =>
				u.Username == model.Username && u.Password == model.Password);

			if (user == default)
				return Unauthorized("Invalid username or password");

			var token = _tokenService.GenerateJwtToken(user.Username, user.Role);

			return Ok(new { token, role = user.Role });
		}
	}

}