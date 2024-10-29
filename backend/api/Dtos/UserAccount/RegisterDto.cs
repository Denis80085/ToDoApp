using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Atributes;

namespace api.Dtos.UserAccount
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "User Name must contain at least 5 characters")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;

        [Required]
        [PasswordPropertyText]
        [MatchAtribute("Password", ErrorMessage = "The passwords do not match.")]
        public string Password_Repeat { get; set; } = string.Empty;
    }
}