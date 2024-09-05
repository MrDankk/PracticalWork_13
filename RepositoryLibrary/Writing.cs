using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class Writing : Checks
    {

        /// <summary>
        /// Запись файла
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        /// <param name="recording"> Что необходимо записать </param>
        public static void FileWriting(string path, string recording)
        {
            FileChecking(path);

            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                if (recording.Trim() != "")
                {
                    streamWriter.WriteLine(recording);
                }
            }
        }
    }
}
