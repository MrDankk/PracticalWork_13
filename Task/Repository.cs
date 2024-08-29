using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Task
{
    abstract class Repository
    {
        private string mainFolder;
        private string customerFile;
        private string depositFile;
        private string notdepositFile;
        private string actionFile;

        public string MainFolder { get { return mainFolder; } }

        public Repository()
        {
            mainFolder = "Customers\\";
            customerFile = "Customer.txt";
            depositFile = "D_Account.txt";
            notdepositFile = "N_Account.txt";
            actionFile = "Action.txt";

            FolderChecking(mainFolder);
        }

        /// <summary>
        /// Получение пути к файлу
        /// </summary>
        /// <param name="customerID"> ID клиента </param>
        /// <param name="item"> 0: клиент,1: Д_Аккаунт,2: Н_Аккаунт,3: Д_Операции,4 Н_Операции</param>
        /// <returns></returns>
        public string GetFilePath(int ID, int item)
        {
            switch (item)
            {
                case 0:
                    return mainFolder + ID + "\\" + customerFile;
                case 1:
                    return mainFolder + ID + "\\" + depositFile;
                case 2:
                    return mainFolder + ID + "\\" + notdepositFile;
                case 3:
                    return mainFolder + ID + "\\" + actionFile;
                default:
                    return null;
            }
        }

        public List<int> GetFoldersNames()
        {
            List<int> index = new List<int>();
            string[] items = Directory.GetDirectories(mainFolder);

            for (int i = 0; i < items.Length; i++)
            {
                index.Add(i);
            }

            return index;
        }

        #region Проверка,чтение и запись файлов

        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        /// <returns> Лист строк файла </returns>
        public List<string> FileReader(string path)
        {
            FileChecking(path);

            List<string> fileItems = new List<string>();

            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    fileItems.Add(line);
                }
            }

            return fileItems;
        }

        /// <summary>
        /// Запись файла
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        /// <param name="recording"> Что необходимо записать </param>
        public void FileWriting(string path, string recording)
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

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="path"></param>
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public void CreateNewCustomerFolder(int id)
        {
            Directory.CreateDirectory(mainFolder + id.ToString());
        }

        /// <summary>
        /// Проверка файла
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        private void FileChecking(string path)
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
        private void FolderChecking(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        #endregion
    }
}
