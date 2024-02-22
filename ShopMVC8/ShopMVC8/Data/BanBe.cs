using System;
using System.Collections.Generic;

namespace ShopMVC8.Data;

public partial class BanBe
{
    public int MaBb { get; set; }

    public string? MaKh { get; set; }

    public int MaHh { get; set; }

    public string? HoTen { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly? NgayGui { get; set; }

    public string? GhiChu { get; set; }

    public virtual HangHoa MaHhNavigation { get; set; } = null!;

    public virtual KhachHang? MaKhNavigation { get; set; }
}
