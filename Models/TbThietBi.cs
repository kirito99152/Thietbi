using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Thietbi.Models;

public partial class TbThietBi
{
    public int IdThietBi { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Chỉ được phép nhập số dương.")]
    public int? IdNguoiSoHuu { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Chỉ được phép nhập số dương.")]

    public int? IdDonViSoHuu { get; set; }

    public int? IdTrangThaiThietBi { get; set; }

    public int? IdLoaiThietBi { get; set; }

    public DateTime? NgayThemThietBi { get; set; }

    public string? TenThietBi { get; set; }

    public string? MoTa { get; set; }

    public string? MaThietBiHv { get; set; }

    public string? MaThietBiNhaSx { get; set; }

    public string? CauHinh { get; set; }

    public string? ViTriDat { get; set; }

    public virtual DmLoaiThietBi? IdLoaiThietBiNavigation { get; set; }

    public virtual DmTrangThaiThietBi? IdTrangThaiThietBiNavigation { get; set; }

    public virtual ICollection<TbLichSuCapPhat> TbLichSuCapPhats { get; set; } = new List<TbLichSuCapPhat>();

    public virtual ICollection<TbLichSuSuaChua> TbLichSuSuaChuas { get; set; } = new List<TbLichSuSuaChua>();

    public virtual ICollection<TbQuanHeThietBi> TbQuanHeThietBiIdThietBiChaNavigations { get; set; } = new List<TbQuanHeThietBi>();

    public virtual ICollection<TbQuanHeThietBi> TbQuanHeThietBiIdThietBiConNavigations { get; set; } = new List<TbQuanHeThietBi>();
}
