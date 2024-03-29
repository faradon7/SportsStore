﻿using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль и пароль подтверждения не совпадают.")]
        [Display(Name = "Подтверждение пароля")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
