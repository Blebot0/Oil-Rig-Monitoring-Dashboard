using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OilRigWebApi.Models;

[Table("Issue")]
public partial class Issue
{
    [Key]
    [StringLength(255)]
    public string IssueId { get; set; } = null!;

    [StringLength(255)]
    public string RigId { get; set; } = null!;

    [StringLength(255)]
    public string UserId { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(255)]
    public string AlertId { get; set; } = null!;

    [StringLength(255)]
    public string? Status { get; set; }

    [ForeignKey("AlertId")]
    [InverseProperty("Issues")]
    public virtual Alert Alert { get; set; } = null!;

    [ForeignKey("RigId")]
    [InverseProperty("Issues")]
    public virtual OilRig Rig { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Issues")]
    public virtual User User { get; set; } = null!;
}
