using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ferma.Service.Services.Interfaces.Area
{
    public interface IProductDeleteServices
    {
        public Task DeleteProduct(int id);
    }
}
