using EcoFarm.Application.Common.Results;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Domain.Common.Values.Constants;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Features.Administration.AccountManagerFeatures.Queries
{
    public class GetListRoleQuery : IQuery<GetListRoleResponse>
    {
    }

    public class GetListRoleResponse
    {
        public KeyValuePair<RoleType, string> Role { get; set; }
    }

    internal class GetListRoleHandler : IQueryHandler<GetListRoleQuery, GetListRoleResponse>
    {
        public async Task<Result<List<GetListRoleResponse>>> Handle(GetListRoleQuery request, CancellationToken cancellationToken)
        {
            var result = EFX.Roles.dctRoles
                .Select(x => new GetListRoleResponse
                {
                    Role = x
                })
                .ToList();
            return new OkResult<List<GetListRoleResponse>>(result);
        }
    }
}
