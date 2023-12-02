using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Domain.Common.Values.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Accounts.Get
{
    public class GetListAccountTypeQuery : IQuery<KeyValuePair<AccountType, string>>
    {
    }

    internal class GetListAccountTypeHandler : IQueryHandler<GetListAccountTypeQuery, KeyValuePair<AccountType, string>>
    {
        public Task<Result<List<KeyValuePair<AccountType, string>>>> Handle(GetListAccountTypeQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success(EFX.AccountTypes.dctAccountType.ToList()));
        }
    }
}
