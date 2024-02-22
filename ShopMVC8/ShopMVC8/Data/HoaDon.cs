using System;
using System.Collections.Generic;

namespace ShopMVC8.Data;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public string MaKh { get; set; } = null!;

    public DateOnly? NgayDat { get; set; }

    public DateOnly? NgayCan { get; set; }

    public DateOnly? NgayGiao { get; set; }

    public string? HoTen { get; set; }

    public string DiaChi { get; set; } = null!;

    public string? CachThanhToan { get; set; }

    public string? CachVanChuyen { get; set; }

    public decimal? PhiVanChuyen { get; set; }

    public int? MaTrangThai { get; set; }

    public string? MaNv { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietHd> ChiTietHds { get; set; } = new List<ChiTietHd>();

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual NhanVien? MaNvNavigation { get; set; }

    public virtual TrangThai? MaTrangThaiNavigation { get; set; }
}
