using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.Area
{
    public class AdminProductEditPostDto
    {
        public int Id { get; set; }
        public string Describe { get; set; }
        public int SubCategoryId { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
    }
    public class AdminProductEditPostDtoValidator : AbstractValidator<AdminProductEditPostDto>
    {
        public AdminProductEditPostDtoValidator()
        {
            RuleFor(x => x.SubCategoryId).NotEmpty().WithMessage("Kategoriya hissəsi boş olmamalıdır.");
            RuleFor(x => x.Id).LessThan(0).WithMessage("Kategoriya hissəsi boş olmamalıdır.");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Elanın ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Elanın adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Elanın adının uzunluğu 100 dən böyük ola bilməz!");
            RuleFor(x => x.Describe).NotEmpty().WithMessage("Elanın təsvir hissəsi boş olmamalıdır.");
        }
    }
}
