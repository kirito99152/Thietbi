using System;
using System.Collections.Generic;

namespace Thietbi.Models;

public partial class DmLoaiThietBi
{
    public int IdLoaiThietBi { get; set; }

    public string? LoaiThietBi { get; set; }

    public virtual ICollection<TbThietBi> TbThietBis { get; set; } = new List<TbThietBi>();
}
