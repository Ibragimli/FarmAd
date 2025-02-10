using FarmAd.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.DTOs.Area
{
    public class UserManagerEditDto
    {
        public string Id { get; set; }
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Password { get; set; }
        public string? RoleId { get; set; }
        public bool IsAdmin { get; set; }
    }
    public class UserManagerEditDtoValidator : AbstractValidator<UserManagerEditDto>
    {
        public UserManagerEditDtoValidator()
        {
            RuleFor(x => x.Fullname).MaximumLength(150).WithMessage("Ad hissəsinin uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.Username).MaximumLength(150).WithMessage("Username hissəsinin uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.Password).MaximumLength(150).WithMessage("Password hissəsinin uzunluğu 150-dən böyük ola bilməz!");
        }
    }
}
