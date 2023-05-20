using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factuacion_MVC.Models;

public partial class TblcategoriaProd
{
    [Key]
    public int IdCategoria { get; set; }
    public string? StrDescripcion { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DtmFechaModifica { get; set; }

    public string? StrUsuarioModifico { get; set; }

    public virtual ICollection<Tblproducto> Tblproductos { get; set; } = new List<Tblproducto>();
}
