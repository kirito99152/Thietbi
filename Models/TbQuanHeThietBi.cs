using System;
using System.Collections.Generic;

namespace Thietbi.Models;

public partial class TbQuanHeThietBi
{
    public int IdQuanHeThietBi { get; set; }

    public int? IdThietBiCha { get; set; }

    public int? IdThietBiCon { get; set; }

    public virtual TbThietBi? IdThietBiChaNavigation { get; set; }

    public virtual TbThietBi? IdThietBiConNavigation { get; set; }
}
