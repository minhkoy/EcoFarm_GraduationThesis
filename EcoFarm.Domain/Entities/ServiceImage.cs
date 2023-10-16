using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{
    [Table("SERVICE_IMAGE")]
    public class ServiceImage : BaseEntity
    {
        [Column("DESCRIPTION")] public string Description { get; set; }
        [Column("IMAGE_DATA")] public byte[] Data { get; set; }
    }
}