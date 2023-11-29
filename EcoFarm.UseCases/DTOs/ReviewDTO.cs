using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.DTOs
{
    public class ReviewDTO
    {
        public string ReviewId { get; set; }
        public string PackageId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string UserFullname { get; set; }
        public string EnterpriseId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? Rating { get; set; }

    }
}
