using System;
using System.Collections.Generic;

namespace Thietbi.Models;

public partial class TbLichSuSuaChua
{
    public int IdLichSuSuaChua { get; set; }

    public int? IdThietBi { get; set; }

    public int? IdNguoiBao { get; set; }

    public int? IdDonViBao { get; set; }

    public int? IdCanBoSua { get; set; }

    public int? IdDonViSua { get; set; }

    public DateTime? ThoiGianBatDau { get; set; }

    public DateTime? ThoiGianKetThuc { get; set; }

    public string? HienTuong { get; set; }

    public string? NguyenNhanXacDinh { get; set; }

    public string? KetQuaSua { get; set; }

    public string? ThongTinSuaChua { get; set; }

    public string? CachSua { get; set; }

    public virtual TbThietBi? IdThietBiNavigation { get; set; }
}
