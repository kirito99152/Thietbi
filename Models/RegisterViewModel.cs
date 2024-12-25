using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Thietbi.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ID phòng.")]
        [Display(Name = "ID Phòng")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Phòng phải là số dương.")]
        public int IdPhong { get; set; }
    }
}