using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class Reader
    {

        /// <summary>
        /// Получить колекцию строк из докумета
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetList(string path)
        {
            Create.File(path);
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
        /// Получить массив строк из документа
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetArray(string path)
        {
            var reader = new Reader();
            Create.File(path);

            string[] array = new string[reader.Length(path)];

            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                int index = 0;

                while ((line = streamReader.ReadLine()) != null)
                {
                    array[index] = line;
                    index++;
                }
            }

            return array;
        }

        /// <summary>
        /// Получение списка папок
        /// </summary>
        /// <returns></returns>
        public static List<string[]> GetFoldersNames(string path)
        {
            List<string[]> folderNames = new List<string[]>();

            string[] customersCount = Directory.GetDirectories(path);

            for (int i = 0; i < customersCount.Length; i++)
            {
                string[] customerFolder = new string[4];

                customerFolder[0] = $"{customersCount[i]}\\N_Account.txt";
                customerFolder[1] = $"{customersCount[i]}\\D_Account.txt";
                customerFolder[2] = $"{customersCount[i]}\\Customer.txt";
                customerFolder[3] = $"{customersCount[i]}\\Action.txt";

                folderNames.Add(customerFolder);
            }

            return folderNames;
        }

        /// <summary>
        /// Количество строк в документе
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private int Length(string path)
        {
            int lenght = 0;

            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    lenght++;
                }
            }

            return lenght;
        }
    }
}
