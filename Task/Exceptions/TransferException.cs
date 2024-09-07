using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task
{
    internal class TransferException : Exception
    {
        public TransferException(string msg)
            : base(msg)
        {}
    }
}
