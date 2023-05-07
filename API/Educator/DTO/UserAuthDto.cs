﻿using System.ComponentModel.DataAnnotations;

namespace Educator.DTO
{
	public class UserAuthDto
	{
		[Required] public string Login { get; set; } = string.Empty; 
		[Required] public string Password { get; set; } = string.Empty;
	}
}