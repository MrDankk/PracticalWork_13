using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal class CustomerException : Exception
    {
        public CustomerException(string msg)
            : base(msg)
        {
        }
}
}
