using EcoFarm.Application.Common.Results;
using EcoFarm.Application.Interfaces.Messagings_Prev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Features.Administration.AccountManagerFeatures.Queries
{
    public class GetAccountInfoQuery : IQuery<GetAccountInfoResponse>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public AccountType? Type { get; set; }
    }

    public class GetAccountInfoResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Fullname { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public AccountType? Type { get; set; }
        public string TypeName { get; set; }
    }

    internal class GetAccountInfoHandler : IQueryHandler<GetAccountInfoQuery, GetAccountInfoResponse>
    {
        public Task<Result<List<GetAccountInfoResponse>>> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
