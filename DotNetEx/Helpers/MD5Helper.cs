using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DotNetEx.Helpers
{
    public class MD5Helper
    {
        public static string MD5Encrypt(string plain)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(plain);

            bs = x.ComputeHash(bs);

            StringBuilder s = new StringBuilder();

            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString().ToUpper();
        }
    }
}
