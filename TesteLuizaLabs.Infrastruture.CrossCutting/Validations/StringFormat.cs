using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Infrastruture.CrossCutting.Validations
{
    public static class StringFormat
    {
        /// <summary>
        /// Método responsavel por validar se o formato do e-mail é valido
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool isValidEmail(this string email)
        {
            try
            {
                string regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
