using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.Area
{
    public class ProductEditPostDto
    {

        public int ProductId { get; set; }
        public string Describe { get; set; }
        public int SubCategoryId { get; set; }
        public string ProductName { get; set; }
    }
    public class ProductEditPostDtoValidator : AbstractValidator<ProductEditPostDto>
    {
        public ProductEditPostDtoValidator()
        {
            RuleFor(x => x.SubCategoryId).NotEmpty().WithMessage("Kategoriya hissəsi boş olmamalıdır.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Kategoriya hissəsi boş olmamalıdır.");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Elanın ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Elanın adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Elanın adının uzunluğu 100 dən böyük ola bilməz!");
            RuleFor(x => x.Describe).NotEmpty().WithMessage("Elanın təsvir hissəsi boş olmamalıdır.");
        }
    }
}
