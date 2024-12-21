using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Thietbi.Models;

public partial class TbQuanHeThietBi
{
    [DisplayName("ID QUAN HỆ THIẾT BỊ")]
    public int IdQuanHeThietBi { get; set; }
    [DisplayName("THIẾT BỊ CHA")]
    public int? IdThietBiCha { get; set; }
    [DisplayName("THIẾT BỊ CON")]
    public int? IdThietBiCon { get; set; }
    [DisplayName("ID THIẾT BỊ CHA")]
    public virtual TbThietBi? IdThietBiChaNavigation { get; set; }
    [DisplayName("ID THIẾT BỊ CON")]
    public virtual TbThietBi? IdThietBiConNavigation { get; set; }
}
