using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.DTOs
{
    public class EnterpriseDTO : AccountDTO
    {
        public string EnterpriseId { get; set; }
        public string EnterpriseName { get; set; }
        public string AccountEmail { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
        public string Hotline { get; set; }
        public AccountDTO AccountInfo { get; set; }
    }
}
