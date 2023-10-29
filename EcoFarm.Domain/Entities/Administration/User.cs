using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EcoFarm.Domain.Entities.Administration;

[Table("USER")]
[Index(nameof(USERNAME), "USER_INDEX1")]
[Index(nameof(EMAIL), "USER_INDEX2")]

public class User : BaseEntity
{
    public string USERNAME { get; set; }
    public string EMAIL { get; set; }
    public bool IS_EMAIL_CONFIRMED { get; set; } = false;
    public string HASHED_PASSWORD { get; set; }
    public DateTime? DATE_OF_BIRTH { get; set; }
    public DateTime? LAST_LOGGED_IN { get; set; }
    public DateTime? LAST_LOGGED_OUT { get; set; }
    //[Column("")]

    //Inverse properties
    [InverseProperty(nameof(Order.UserOrdered))]
    public virtual ICollection<Order> Orders { get; set; }
}