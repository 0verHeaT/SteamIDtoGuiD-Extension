using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace be_guid
{
    public class DllEntry
    {
        [DllExport("_RVExtension@12", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
        public static void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string input)
        {
            Int64 id = 0;
            byte[] parts = { 0x42, 0x45, 0, 0, 0, 0, 0, 0, 0, 0 };

            outputSize--;

            id = Convert.ToInt64(input);
            
            byte counter = 2;
            do
            {
                parts[counter++] = (byte)(id & 0xFF);
            } while ((id >>= 8) > 0);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] beHash = md5.ComputeHash(parts);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < beHash.Length; i++)
            {
                sb.Append(beHash[i].ToString("x2"));
            }
            
            output.Append(sb.ToString());
        }
    }
}