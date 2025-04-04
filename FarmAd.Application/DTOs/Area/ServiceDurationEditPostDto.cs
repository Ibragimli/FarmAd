using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.Area
{
    public class ServiceDurationEditPostDto
    {
        public int Id { get; set; }
        public int ServiceType { get; set; }
        public decimal? Amount { get; set; }
        public int Duration { get; set; }
    }
    public class ServiceDurationEditPostDtoValidator : AbstractValidator<ServiceDurationEditPostDto>
    {
        public ServiceDurationEditPostDtoValidator()
        {
        }
    }
}
