using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenHandler.Models
{
    public class AccountTokenData
    {
        public string EntityId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string AccountTypeName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public DateTime ExpireDateTime { get; set; }
    }
}
