using System;
using System.Collections.Generic;
using System.Text;

namespace shared.utility.infrastructure {
    public class Encrypter {
        public static string Md5Password(string password) {
            if (string.IsNullOrWhiteSpace(password))
                return null;
            var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(password);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var t in hash) {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
