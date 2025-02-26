using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Product.ProductAuthenticationCommand
{
    public class ProductAuthenticationCommandResponse
    {
        public bool Succeed { get; set; }
        public string Username { get; set; }
    }
}
