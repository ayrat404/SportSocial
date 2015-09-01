using System.Security.Cryptography;
using System.Text;

namespace BLL.Common.Helpers
{
    public static class Hasher
    {
        public static string Md5(string stringToHash)
        {
            return Md5(Encoding.ASCII.GetBytes(stringToHash));
        }

        public static string Md5(byte[] bytesToHash)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] bSignature = md5.ComputeHash(bytesToHash);
            var sbSignature = new StringBuilder();
            foreach (byte b in bSignature)
                sbSignature.AppendFormat("{0:x2}", b);
            return sbSignature.ToString();
        }
    }
}