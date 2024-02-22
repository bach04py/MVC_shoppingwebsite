using System;
using System.Collections.Generic;

namespace ShopMVC8.Data;

public partial class PhanQuyen
{
    public int MaPq { get; set; }

    public string? MaPb { get; set; }

    public int? MaTrang { get; set; }

    public int? Them { get; set; }

    public int? Sua { get; set; }

    public int? Xoa { get; set; }

    public int? Xem { get; set; }

    public virtual PhongBan? MaPbNavigation { get; set; }

    public virtual TrangWeb? MaTrangNavigation { get; set; }
}
