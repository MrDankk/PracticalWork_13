using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public static class BankExtension
    {
        /// <summary>
        /// Расчёт процентво за месяц
        /// </summary>
        /// <param name="balance"> Баланс </param>
        /// <param name="type"> Тип Аккаунта </param>
        /// <returns></returns>
        public static long MonthlyInterest(this long balance, bool type)
        {
            long month = balance;

            if (type)
            {
                float percentages = (float)(balance * 1.12) / 12;
                month += (long)percentages;
            }

            return month;
        }

        /// <summary>
        /// Расчёт процетов за год
        /// </summary>
        /// <param name="balance"> Баланс </param>
        /// <param name="type"> Тип аккаунта </param>
        /// <returns></returns>
        public static long AnnualInterest(this long balance, bool type)
        {
            long year = balance;

            if (type)
            {
                for (int i = 0; i < 12; i++)
                {
                    float percentages = (float)(year * 1.12) / 12;
                    year += (long)percentages;
                }
            }
            else
            {
                float percentages = (float)(year * 0.12);
                year += (long)percentages;
            }

            return year;
        }

        /// <summary>
        /// Получение пути к файлу аккаунта
        /// </summary>
        /// <param name="type"> Тип аккаунта </param>
        /// <param name="id"> Индекс </param>
        /// <param name="list"> Коллекция всех путей </param>
        /// <returns></returns>
        public static string AccountPath(this bool type, int id, List<string[]> list)
        {
            string[] path = list[id];

            if (type)
            {
                return path[1];
            }
            else
            {
                return path[0];
            }
        }

        /// <summary>
        /// Создание пути к файлу нового аккаунта
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string NewAccountPath(this bool type, int id)
        {
            if (type)
                return $"Customers\\{id}\\D_Account.txt";
            else
                return $"Customers\\{id}\\N_Account.txt";
        }

        /// <summary>
        /// Получение пути к файлу клиента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string CustomerPath(this int id, List<string[]> list)
        {
            string[] path = list[id];

            return path[2];
        }

        /// <summary>
        /// Создание пути к файлу нового клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string NewCustomerPath(this int id)
        {
            return $"Customers\\{id}\\Customer.txt";
        }

        /// <summary>
        /// Добавление объекта в коллекцию
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"> Коллекция </param>
        /// <param name="item"> Объект </param>
        public static void AddTo<T> (this T item, ObservableCollection<T> list)
        {
            list.Add(item);
        }
        public static void AddTo<T>(this T item, List<T> list)
        {
            list.Add(item);
        }

        /// <summary>
        /// Удаление объекта из коллекции
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"> Объект </param>
        /// <param name="list"> Коллекция </param>
        public static void DeleteFrom<T> (this T item, ObservableCollection<T> list)
        {
            list.Remove(item);
        }
        public static void DeleteFrom<T>(this T item, List<T> list)
        {
            list.Remove(item);
        }

    }
}
