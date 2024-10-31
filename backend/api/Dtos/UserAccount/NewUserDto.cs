using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.UserAccount
{
    public class NewUserDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "User Name must contain at least 5 characters")]
        public string? UserName { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;
    }
}