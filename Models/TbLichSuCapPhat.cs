using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Thietbi.Models;

public partial class TbLichSuCapPhat
{
    [DisplayName("ID LỊCH SỬ CẤP PHÁT")]
    
    public int IdLichSuCapPhat { get; set; }
    [DisplayName("ID THIẾT BỊ")]
    
    public int? IdThietBi { get; set; }
    [DisplayName("ID NGƯƠI ĐƯỢC GIAO")]
    [Range(1, int.MaxValue, ErrorMessage = "Chỉ được phép nhập số dương.")]
    public int? IdNguoiDuocGiao { get; set; }
    [DisplayName("ID ĐƠN VỊ ĐƯỢC GIAO")]
    [Range(1, int.MaxValue, ErrorMessage = "Chỉ được phép nhập số dương.")]
    public int? IdDonViDuocGiao { get; set; }
    [DisplayName("ID NGƯỜI CẤP PHÁT")]
    [Range(1, int.MaxValue, ErrorMessage = "Chỉ được phép nhập số dương.")]
    public int? IdNguoiCapPhat { get; set; }
    [DisplayName("THỜI GIAN CẤP PHÁT")]
    public DateTime? ThoiGianCapPhat { get; set; }
    [DisplayName("THỜI GIAN THU HỒI")]
    public DateTime? ThoiGianThuHoi { get; set; }
    [DisplayName("GHI CHÚ")]
    public string? GhiChu { get; set; }
    [DisplayName("THIẾT BỊ")]
    public virtual TbThietBi? IdThietBiNavigation { get; set; }
    
}
