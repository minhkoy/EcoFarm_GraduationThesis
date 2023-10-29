﻿using EcoFarm.Domain.Common;
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
        public string DESCRIPTION { get; set; }
        public byte[] DATA { get; set; }
    }
}