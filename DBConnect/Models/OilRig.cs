using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBConnect.Models;

[Table("OilRig")]
public partial class OilRig
{
    [Key]
    [StringLength(255)]
    public string RigId { get; set; } = null!;

    [StringLength(255)]
    public string RigName { get; set; } = null!;

    public double Temperature { get; set; }

    public double Pressure { get; set; }

    [StringLength(255)]
    public string ProductionRate { get; set; } = null!;

    [StringLength(255)]
    public string? Status { get; set; }

    [StringLength(255)]
    public string? RigLocation { get; set; }

    [InverseProperty("Rig")]
    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    [InverseProperty("Rig")]
    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
