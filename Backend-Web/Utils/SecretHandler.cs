using System;
using System.Security.Cryptography;

namespace Backend_Web.Utils
{
    public static class SecretHandler
    {
        internal static string Secret = Convert.ToBase64String(new HMACSHA256().Key);
    }
}