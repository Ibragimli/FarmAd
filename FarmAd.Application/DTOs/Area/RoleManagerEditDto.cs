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
    public class RoleManagerEditDto
    {
        public string RoleName { get; set; }
        public string Id { get; set; }
    }
    public class RoleManagerEditDtoValidator : AbstractValidator<RoleManagerEditDto>
    {
        public RoleManagerEditDtoValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role hissəsi boş olmamalıdır.").MinimumLength(1).WithMessage("Role hissəsinin uzunluğu 1-dən az ola bilməz!").MaximumLength(150).WithMessage("Role hissəsinin uzunluğu 150-dən böyük ola bilməz!");

        }
    }
}
