using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Intellisense
{
    class CakeFunctions
    {
        public static IEnumerable<string> Functions =
            new[]
            {
                "CopyFiles",
                "CopyFile",
                "RemoveFile",
                "RemoveFiles"
            };
    }
}
