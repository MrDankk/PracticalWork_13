using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal class SelectException : Exception
    {
        public SelectException(string msg)
            : base(msg)
        {}
    }
}
