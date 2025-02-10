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
    public class UserManagerCreateDto
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public bool IsAdmin { get; set; }

    }
    public class UserManagerCreateDtoValidator : AbstractValidator<UserManagerCreateDto>
    {
        public UserManagerCreateDtoValidator()
        {
            RuleFor(x => x.Fullname).NotEmpty().WithMessage("Ad hissəsi boş olmamalıdır.").MinimumLength(4).WithMessage("Ad hissəsinin uzunluğu 4-dən az ola bilməz!").MaximumLength(150).WithMessage("Ad hissəsinin uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username hissəsi boş olmamalıdır.").MinimumLength(4).WithMessage("Username hissəsinin uzunluğu 4-dən az ola bilməz!").MaximumLength(150).WithMessage("Username hissəsinin uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password hissəsi boş olmamalıdır.").MinimumLength(4).WithMessage("Password hissəsinin uzunluğu 4-dən az ola bilməz!").MaximumLength(150).WithMessage("Password hissəsinin uzunluğu 150-dən böyük ola bilməz!");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username hissəsi boş olmamalıdır.");
        }
    }
}
