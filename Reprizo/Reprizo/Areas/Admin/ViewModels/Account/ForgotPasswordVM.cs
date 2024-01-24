﻿using System.ComponentModel.DataAnnotations;

namespace Reprizo.Areas.Admin.ViewModels.Account
{
	public class ForgotPasswordVM
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
	}
}
