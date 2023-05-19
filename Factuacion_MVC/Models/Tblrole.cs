using System;
using System.Collections.Generic;

namespace Factuacion_MVC.Models;

public partial class Tblrole
{
    public int IdRolEmpleado { get; set; }

    public string? StrDescripcion { get; set; }

    public virtual ICollection<Tblempleado> Tblempleados { get; set; } = new List<Tblempleado>();
}
