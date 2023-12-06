using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.DTOs
{
    public class ActivityDTO
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PackageId { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string MediaUrls { get; set; }
        public FarmingPackageDTO Package { get; set; }
        

    }
}
