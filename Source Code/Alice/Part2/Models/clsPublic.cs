using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Part2.Models
{
    public static class clsPublic
    {
        public static int Prime = -1;
        public static List<int> CyclicGroupNumbers;
        public static int g = -1;

        public static int PrivateKeyAlice;
        public static int PublicKeyAlice;
        public static int PublicKeyBob;

        public static int SharedKeyAlice;
        public static int[] SecretKeyAlice;

        public static string Message;
    }
}