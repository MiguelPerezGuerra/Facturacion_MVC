using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factuacion_MVC.Models;

public partial class Tblempleado
{
    [System.ComponentModel.DataAnnotations.Key]
    public int IdEmpleado { get; set; }

    public string StrNombre { get; set; } = null!;

    public long NumDocumento { get; set; }

    public string? StrDireccion { get; set; }

    public string? StrTelefono { get; set; }

    public string? StrEmail { get; set; }

    public int? IdRolEmpleado { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DtmIngreso { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DtmRetiro { get; set; }

    public string? StrDatosAdicionales { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DtmFechaModifica { get; set; }

    public string? StrUsuarioModifico { get; set; }

    public virtual Tblrole? IdRolEmpleadoNavigation { get; set; }

    public virtual ICollection<Tblfactura> Tblfacturas { get; set; } = new List<Tblfactura>();

    public virtual ICollection<Tblseguridad> Tblseguridads { get; set; } = new List<Tblseguridad>();
}
