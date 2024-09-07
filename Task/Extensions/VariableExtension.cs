using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public static class VariableExtension
    {
        /// <summary>
        /// Разделение строки на массив слов
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] Separation(this string text)
        {
            return text.Split('#');
        }

        /// <summary>
        /// Соединение массива строк
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string Join(this string[] array)
        {
            return string.Join("#", array);
        }

        /// <summary>
        /// Перевод строки в int
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int IntParse(this string text)
        {
            return int.Parse(text);
        }

        /// <summary>
        /// Перевод строки в long
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static long LongParse(this string text)
        {
            return long.Parse(text);
        }

        /// <summary>
        /// Перевод из строки в bool
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool BoolType(this string text)
        {
            if (text == "1")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Перевод из bool в int
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int BoolToInt(this bool type)
        {
            if(type)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Перевод из bool в string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string BoolToString(this bool type)
        {
            if (type)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
    }
}
