using System;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC8.ViewModels
{
    public class QuanliHangHoaVM
    {
        [Display(Name = "Mã hàng hoá")]
        public int MaHh { get; set; }

        [Display(Name = "Tên hàng hoá")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string? TenHh { get; set; }

        [Display(Name = "Tên loại")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string? TenAlias { get; set; }

        [Display(Name = "Mã loại")]
        public int MaLoai { get; set; }

        [Display(Name = "Mô tả đơn vị")]
        [MaxLength(60, ErrorMessage = "Tối đa 60 kí tự")]
        public string? MoTaDonVi { get; set; }

        [Display(Name = "Đơn giá")]
        [RegularExpression(@"^\d{0,10}(\.\d{1,2})?$", ErrorMessage = "Đơn giá không hợp lệ")]
        public decimal? DonGia { get; set; }

        [Display(Name = "Hình")]
        public string? Hinh { get; set; }

        [Display(Name = "Ngày sản xuất")]
        [DataType(DataType.Date, ErrorMessage = "Ngày sản xuất không hợp lệ")]
        public DateOnly  NgaySx { get; set; }

        [Display(Name = "Giảm giá")]
        [Range(0, 100, ErrorMessage = "Giảm giá phải nằm trong khoảng từ 0 đến 100")]
        public decimal? GiamGia { get; set; }

        [Display(Name = "Mô tả")]
        [MaxLength(60, ErrorMessage = "Tối đa 60 kí tự")]
        public string? MoTa { get; set; }

        [Display(Name = "Mã nhà cung cấp")]
        [MaxLength(60, ErrorMessage = "Tối đa 60 kí tự")]
        public string? MaNcc { get; set; }
    }
}