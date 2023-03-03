using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CREL_BE.Helpers
{
    public static class BitHelper
    {
        public static bool GetBit(int n, int i) => ((n >> i) & 1) == 1;
        public static int SetBit(int n, int i, bool bit) => bit ? ~((~n) & (~(1 << i))) : n & (~(1 << i));
    }
}