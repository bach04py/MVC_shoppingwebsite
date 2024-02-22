using System.ComponentModel.DataAnnotations;

namespace ShopMVC8.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage ="*")]
        [MaxLength(20,ErrorMessage ="Tối đa 20 kí tự")]
        public required string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

    }
}
