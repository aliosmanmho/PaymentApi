using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.Extensions
{
    public static class PaymentExtension
    {
        public static int GetBimNumber(this string str)
        {
            if (str == null)
            {
                throw new Exception("Bin Number Null!");
            }
            var trim = str.Trim();
            if (trim.Length == 0)
                throw new Exception("Bin Number Empty!");
            if (trim.Length < 6)
                throw new Exception("Bin Numer Length Short!");
            bool succes = int.TryParse(trim.Substring(0, 6), out int result);
            if (!succes)
                throw new Exception("Bin Number Non Numaric!");
            return result;
        }
    }
}
