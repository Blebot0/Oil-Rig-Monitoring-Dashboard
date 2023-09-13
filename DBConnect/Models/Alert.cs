using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBConnect.Models;

[Table("Alert")]
public partial class Alert
{
    [Key]
    [StringLength(255)]
    public string AlertId { get; set; } = null!;

    [StringLength(255)]
    public string AlertSeverity { get; set; } = null!;

    [StringLength(255)]
    public string AlertMessage { get; set; } = null!;

    [StringLength(255)]
    public string AlertStatus { get; set; } = null!;

    [StringLength(255)]
    public string RigId { get; set; } = null!;

    [InverseProperty("Alert")]
    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    [ForeignKey("RigId")]
    [InverseProperty("Alerts")]
    public virtual OilRig Rig { get; set; } = null!;
}
