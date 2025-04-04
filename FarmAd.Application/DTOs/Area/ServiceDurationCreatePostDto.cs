using FarmAd.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.Area
{
    public class ServiceDurationCreatePostDto
    {
        public int ServiceType { get; set; }
        public decimal? Amount { get; set; }
        public int Duration { get; set; }
    }
    public class ServiceDurationCreatePostDtoValidator : AbstractValidator<ServiceDurationCreatePostDto>
    {
        public ServiceDurationCreatePostDtoValidator()
        {
        }
    }
}
