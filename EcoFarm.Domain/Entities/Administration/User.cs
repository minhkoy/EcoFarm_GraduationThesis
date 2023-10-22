using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EcoFarm.Domain.Entities.Administration;

[Table("USER")]
[Index(nameof(Username), "USER_INDEX1")]
[Index(nameof(Email), "USER_INDEX2")]

public class User : BaseEntity
{
    [Column("USERNAME")]
    public string Username { get; set; }
    [Column("EMAIL")]
    public string Email { get; set; }
    [Column("IS_EMAIL_CONFIRMED")]
    public bool IsEmailConfirmed { get; set; } = false;
    [Column("HASHED_PASSWORD")]
    public string HashedPassword { get; set; }
    [Column("DATE_OF_BIRTH")]
    public DateTime? DateOfBirth { get; set; }
    //[Column("")]

    //Inverse properties
    [InverseProperty(nameof(Order.UserOrdered))]
    public virtual ICollection<Order> Orders { get; set; }
}