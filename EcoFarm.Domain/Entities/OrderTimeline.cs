using EcoFarm.Domain.Common;
using EcoFarm.Domain.Common.Values.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities
{
    [Table("ORDER_TIMELINE")]
    public class OrderTimeline : BaseNonExtendedEntity
    {
        public string ORDER_ID { get; set; }
        public OrderStatus STATUS { get; set; }
        [NotMapped]
        public string StatusName { get => EFX.OrderStatuses.dctOrderStatus[STATUS]; }
        public DateTime? TIME { get; set; }
        [ForeignKey(nameof(ORDER_ID))]
        [InverseProperty(nameof(Order.OrderTimelines))]
        public virtual Order OrderInfo { get; set; }
    }
}
