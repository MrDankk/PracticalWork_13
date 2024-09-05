using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RepositoryLibrary
{
    public class Delete
    {
        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="path"></param>
        public static void File(string path)
        {
            System.IO.File.Delete(path);
        }

        /// <summary>
        /// Удаление папки
        /// </summary>
        /// <param name="path"></param>
        public static void Folder(string path)
        {
            Directory.Delete(path);
        }
    }
}
