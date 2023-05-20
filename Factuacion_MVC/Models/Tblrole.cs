using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factuacion_MVC.Models;

public partial class Tblrole
{
    [Key]
    public int IdRolEmpleado { get; set; }

    [Display(Name ="Rol")]
    public string? StrDescripcion { get; set; }

    public virtual ICollection<Tblempleado> Tblempleados { get; set; } = new List<Tblempleado>();
}
