using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.DTOs
{
    public class EnterpriseDTO
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string AccountUsername { get; set; }
        public string Name { get; set; }
        public string AccountEmail { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
        public string Hotline { get; set; }
    }
}
