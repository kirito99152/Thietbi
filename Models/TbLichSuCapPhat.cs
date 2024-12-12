using System;
using System.Collections.Generic;

namespace Thietbi.Models;

public partial class TbLichSuCapPhat
{
    public int IdLichSuCapPhat { get; set; }

    public int? IdThietBi { get; set; }

    public int? IdNguoiDuocGiao { get; set; }

    public int? IdDonViDuocGiao { get; set; }

    public int? IdNguoiCapPhat { get; set; }

    public DateTime? ThoiGianCapPhat { get; set; }

    public DateTime? ThoiGianThuHoi { get; set; }

    public string? GhiChu { get; set; }

    public virtual TbThietBi? IdThietBiNavigation { get; set; }
}
