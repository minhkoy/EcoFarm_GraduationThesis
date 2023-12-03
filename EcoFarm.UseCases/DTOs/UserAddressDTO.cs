using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.DTOs
{
    public class UserAddressDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string AddressDescription { get; set; }
        public string ReceiverName { get; set; }
        public string AddressPhone { get; set; }
        public bool? IsPrimary { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
