using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class Create : IChecking
    {
        public string[] folderName;

        public Create()
        {
            folderName = new string[] { "N_Account.txt", "D_Account.txt", "Customer.txt", "Action.txt" };
        }

        public static void File(string path)
        {
            var create = new Create();

            if(!create.CheckFile(path))
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                fileStream.Close();
            }
        }

        /// <summary>
        /// Создание новой папки
        /// </summary>
        /// <param name="id"></param>
        public static void Folder(string path)
        {
            var create = new Create();

            if(!create.CheckFolder(path))
                Directory.CreateDirectory(path);
        }

        public static void CreateAllFile(int id)
        {
            string path = $"Customers\\{id}\\";

            var create = new Create();

            File(path + create.folderName[0]);
            File(path + create.folderName[1]);
            File(path + create.folderName[2]);
            File(path + create.folderName[3]);
        }

        /// <summary>
        /// Проверка наличия файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CheckFile(string path)
        {
            if (System.IO.File.Exists(path))
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
            if (Directory.Exists(path))
                return true;

            return false;
        }
    }
}
