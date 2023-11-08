using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{

    [Table("USER_REVIEW")]
    public class UserReview
    {
        public string USER_ID { get; set; }
        public string SERVICE_ID { get; set; }
        public string COMMENT { get; set; }
        public int? RATING { get; set; }
    }
}
