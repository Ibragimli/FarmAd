using FarmAd.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IBalanceIncreaseServices
    {
        public Task CheckBalanceIncrease(BalanceDto balanceDto);
        public Task BalanceIncrease(BalanceDto balanceDto);

    }
}
