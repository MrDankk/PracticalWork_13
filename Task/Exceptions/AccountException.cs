using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal class AccountException : Exception
    {
        public AccountException(string msg) 
            : base (msg)
        {}
    }
}
