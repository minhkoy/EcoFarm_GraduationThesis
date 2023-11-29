using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.DTOs
{
    public class AccountDTO
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool? IsEmailConfirmed { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        public string AccountType { get; set; }
        public bool? IsActive { get; set; }
        public string LockedReason { get; set; }
        public string AvatarUrl { get; set; }
    }
}
