using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Orders.Get
{
    public class GetListOrderQuery : IQuery<OrderDTO>
    {
        public string UserId { get; set; }
        public string EnterpriseId { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        /// <summary>
        /// Keyword cho mã đơn hàng (code) và note
        /// </summary>
        public string Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = EFX.DefaultPageSize;
    }

    internal class GetListOrderHandler : IQueryHandler<GetListOrderQuery, OrderDTO>
    {
        public Task<Result<List<OrderDTO>>> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
