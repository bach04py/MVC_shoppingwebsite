using AutoMapper;
using ShopMVC8.Data;
using ShopMVC8.ViewModels;

namespace ShopMVC8.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() {
            CreateMap<RegisterVM,KhachHang>();
            
        }

    }
}
