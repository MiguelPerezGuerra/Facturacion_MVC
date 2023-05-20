using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factuacion_MVC.Models;

public partial class Tblseguridad
{
    [Key]
    public int IdSeguridad { get; set; }

    [Display(Name = "Empleado")]
    public int IdEmpleado { get; set; }

    [Display(Name = "Usuario")]
    public string? StrUsuario { get; set; }

    [Display(Name = "Password")]
    public string? StrClave { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DtmFechaModifica { get; set; }

    public string? StrUsuarioModifico { get; set; }

    [Display(Name = "Empleado")]
    public virtual Tblempleado IdEmpleadoNavigation { get; set; } = null!;
}
