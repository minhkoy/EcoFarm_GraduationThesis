using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.DTOs
{
    public class UserDTO : AccountDTO
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public GenderEnum? Gender { get; set; }
        public string GenderName { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
