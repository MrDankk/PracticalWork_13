using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    abstract public class Checks
    {
        /// <summary>
        /// Проверка файла
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        protected static void FileChecking(string path)
        {
            if (!File.Exists(path))
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                fileStream.Close();
            }
        }

        /// <summary>
        /// Проверка папки
        /// </summary>
        /// <param name="folderPath"></param>
        protected static void FolderChecking(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
