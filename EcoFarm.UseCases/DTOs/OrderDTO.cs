using EcoFarm.Domain.Common.Values.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.DTOs
{
    public class OrderDTO //: EnterpriseDTO
    {
        public string OrderId { get; set; }
        public string OrderCode { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public List<ProductDTO> ListProducts { get; set; }
        public string Note { get; set; }
        public string SellerEnterpriseId { get; set; }
        public string SellerEnterpriseName { get; set; }
        public string AddressId { get; set; }
        public string AddressDescription { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? TotalQuantity { get; set; }
        public CurrencyType? Currency { get; set; }
        public string CurrencyName { get => Currency.HasValue ? Currency.Value.ToString() : string.Empty;}
        public OrderPaymentMethod? PaymentMethod { get; set; }
        public string PaymentMethodName { get => PaymentMethod.HasValue ? EFX.PaymentMethods.dctPaymentMethod[PaymentMethod.Value] : string.Empty; }
        public OrderStatus? Status { get; set; }
        public string StatusName
        {
            get => Status.HasValue ? EFX.OrderStatuses.dctOrderStatus[Status.Value] : string.Empty;
        }
    }
}
