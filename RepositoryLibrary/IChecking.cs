using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    internal interface IChecking
    {
        bool CheckFolder(string path);

        bool CheckFile(string path);
    }
}
