﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Thietbi.Models;

public partial class TbLichSuSuaChua
{
    [DisplayName("ID LỊCH SỬ SỬA CHỮA")]
    public int IdLichSuSuaChua { get; set; }
    [DisplayName("ID THIẾT BỊ")]
    public int? IdThietBi { get; set; }
    [DisplayName("ID NGƯỜI BÁO")]
    [Range(1, int.MaxValue, ErrorMessage = "Chỉ được phép nhập số dương.")]
    public int? IdNguoiBao { get; set; }
    [DisplayName("ID ĐƠN VỊ BÁO")]
    [Range(1, int.MaxValue, ErrorMessage = "Chỉ được phép nhập số dương.")]
    public int? IdDonViBao { get; set; }
    [DisplayName("ID CÁN BỘ SỬA")]
    [Range(1, int.MaxValue, ErrorMessage = "Chỉ được phép nhập số dương.")]
    public int? IdCanBoSua { get; set; }
    [DisplayName("ID ĐƠN VỊ SỬA")]
    [Range(1, int.MaxValue, ErrorMessage = "Chỉ được phép nhập số dương.")]
    public int? IdDonViSua { get; set; }
    [DisplayName("THỜI GIAN BẮT ĐẦU")]
    public DateTime? ThoiGianBatDau { get; set; }
    [DisplayName("THỜI GIAN KẾT THÚC")]
    public DateTime? ThoiGianKetThuc { get; set; }
    [DisplayName("HIỆN TƯỢNG")]
    public string? HienTuong { get; set; }
    [DisplayName("NGUYÊN NHÂN XÁC ĐỊNH")]
    public string? NguyenNhanXacDinh { get; set; }
    [DisplayName("KẾT QUẢ SỬA")]
    public string? KetQuaSua { get; set; }
    [DisplayName("THÔNG TIN SỬA CHỮA")]
    public string? ThongTinSuaChua { get; set; }
    [DisplayName("CÁCH SỬA")]
    public string? CachSua { get; set; }
    [DisplayName("THIẾT BỊ")]
    public virtual TbThietBi? IdThietBiNavigation { get; set; }
}
