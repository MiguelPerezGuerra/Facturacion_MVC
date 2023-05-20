using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace Factuacion_MVC.Models;

public partial class Tblcliente
{
    [Key]
    public int IdCliente { get; set; }

    public string? StrNombre { get; set; }

    public long? NumDocumento { get; set; }

    public string? StrDireccion { get; set; }

    public string? StrTelefono { get; set; }

    public string? StrEmail { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DtmFechaModifica { get; set; }

    public string? StrUsuarioModifica { get; set; }

    public virtual ICollection<Tblfactura> Tblfacturas { get; set; } = new List<Tblfactura>();
}
