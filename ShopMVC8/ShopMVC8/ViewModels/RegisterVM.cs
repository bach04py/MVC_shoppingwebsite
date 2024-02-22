using ShopMVC8.Data;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC8.ViewModels
{
    public class RegisterVM
    {
        [Display(Name ="Tên đăng nhập")]   
       
        [MaxLength(20,ErrorMessage ="Tối đa 20 kí tự")]
        public string? MaKh { get; set; }
        [Display(Name = "Mật khẩu")]
    
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        [DataType(DataType.Password)]
        public string? MatKhau { get; set; }
        [Display(Name = "Họ và tên")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string? HoTen { get; set; }
        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; } = true;

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateOnly? NgaySinh { get; set; }
        [MaxLength(60, ErrorMessage = "Tối đa 60 kí tự")]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }
        [MaxLength(24, ErrorMessage = "Tối đa 24 kí tự")]
        [RegularExpression(@"0[9875]\d{8}",ErrorMessage ="Không đúng định dạng")]
        [Display(Name = "Số điện thoại")]
        public string? DienThoai { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Không đúng định dạng")]
        public string? Email { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string? Hinh { get; set; }

    
    }
}

