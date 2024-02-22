using System.Linq.Expressions;
using System.Text;

namespace ShopMVC8.Helper
{
    public class Util
    {
        public static string UpLoadHinh(IFormFile Hinh, string folder)
        { try
            { var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, Hinh.FileName);
                using (var myfile = new FileStream(fullpath, FileMode.CreateNew))
                {

                    Hinh.CopyTo(myfile);
                }
                return Hinh.FileName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
 
        public static string GenerateRandomkey(int length =5)
        {
            var pattern = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOASDFGHJKLZXCVBNM!@#$%^&*()_+";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0;i<length;i++)
            {
                sb.Append(pattern[rd.Next(0,pattern.Length)]);
            }    
            return sb.ToString();
        }
    }
}
