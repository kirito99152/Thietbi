using System;
using System.Collections.Generic;

namespace Thietbi.Models;

public partial class DmTrangThaiThietBi
{
    public int IdTrangThaiThietBi { get; set; }

    public string? TrangThaiThietBi { get; set; }

    public virtual ICollection<TbThietBi> TbThietBis { get; set; } = new List<TbThietBi>();
}
