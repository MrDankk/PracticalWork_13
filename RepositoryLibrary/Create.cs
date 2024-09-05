using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class Create
    {
        public static void File(string path)
        {
            System.IO.File.Create(path);
        }

        /// <summary>
        /// Создание новой папки
        /// </summary>
        /// <param name="id"></param>
        public static void Folder(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
