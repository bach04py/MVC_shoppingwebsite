using System;
using System.Collections.Generic;

namespace ShopMVC8.Data;

public partial class Vchitiethoadon
{
    public int MaCt { get; set; }

    public int MaHd { get; set; }

    public int MaHh { get; set; }

    public decimal? DonGia { get; set; }

    public int? SoLuong { get; set; }

    public decimal? GiamGia { get; set; }

    public string TenHh { get; set; } = null!;
}
