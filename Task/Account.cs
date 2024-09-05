using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal class Account
    {
        private int id;
        private int number;
        private long balance;
        private bool type;

        public int ID {  get { return id; } }
        public int Number { get { return number; } }
        public long Balance { get { return balance; } 
                              set { balance = value; }}
        public bool Type { get { return type; } }

        public Account(int id,int number,long balance,bool type) 
        { 
            this.id = id;
            this.number = number;
            this.balance = balance;
            this.type = type;
        }

        public Account(Account account)
        {
            this.id = account.id;
            this.number = account.number;
            this.balance = account.balance;
            this.type = account.type;
        }

        /// <summary>
        /// Преобразование в массив
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string[] AccountArray()
        {
            return new string[] { ID.ToString(), 
                                  Number.ToString(), 
                                  Balance.ToString(), 
                                  Type.BoolToString() };
        }

        /// <summary>
        /// Добавление баланса
        /// </summary>
        /// <param name="account"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static Account operator + (Account account, long sum)
        {
            account.Balance = account.Balance + sum;
            return account;
        }

        /// <summary>
        /// Вычитание баланса
        /// </summary>
        /// <param name="account"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static Account operator - (Account account, long sum)
        {
            account.Balance = account.Balance - sum;
            return account;
        }
    }
}
