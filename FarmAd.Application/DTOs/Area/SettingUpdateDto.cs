using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.Area
{
    public class SettingUpdateDto
    {
        public string Value { get; set; }
        public string Key { get; set; }
        public IFormFile KeyImageFile { get; set; }
        public int Id { get; set; }

        public class CreatePostDtoValidator : AbstractValidator<SettingUpdateDto>
        {
            public CreatePostDtoValidator()
            {
                RuleFor(x => x.Value).NotEmpty().WithMessage("boş olmamalıdır.").MaximumLength(750);
                RuleFor(x => x.KeyImageFile).NotEmpty().WithMessage("boş olmamalıdır.");
            }
        }
    }
}
