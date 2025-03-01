using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.User
{
    public class ProductAuthenticationDto
    {
        public string Code { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
    public class ProductAuthenticationDtoValidator : AbstractValidator<ProductAuthenticationDto>
    {
        public ProductAuthenticationDtoValidator()
        {
        }
    }
}
