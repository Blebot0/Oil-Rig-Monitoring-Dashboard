using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OilRigWebApi.Models;

[Table("User")]
public partial class User
{
    [Key]
    [StringLength(255)]
    public string UserId { get; set; } = null!;

    [StringLength(255)]
    public string Username { get; set; } = null!;

    [Column("Department ")]
    [StringLength(255)]
    public string? Department { get; set; }

    [StringLength(255)]
    public string? Designation { get; set; }

    public int? ManagementLevel { get; set; }

    [StringLength(500)]
    public string Password { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
