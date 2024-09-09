using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RepositoryLibrary
{
    public class Delete : IChecking
    {
        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="path"></param>
        public static void File(string path)
        {
            var del = new Delete();

            if(del.CheckFile(path))
            { 
                System.IO.File.Delete(path); 
            }
        }

        /// <summary>
        /// Удаление папки
        /// </summary>
        /// <param name="path"></param>
        public static void Folder(string path)
        {
            var del = new Delete();

            if(del.CheckFolder(path))
            { 
                Directory.Delete(path); 
            }
        }

        /// <summary>
        /// Проверка наличия файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CheckFile(string path)
        {
            if(System.IO.File.Exists(path))
                return true;

            return false;
        }

        /// <summary>
        /// Проверка наличия папки
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CheckFolder(string path)
        {
            if(Directory.Exists(path))
                return true;

            return false;
        }
    }
}
