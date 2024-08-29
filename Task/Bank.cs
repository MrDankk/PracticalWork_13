using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Task
{
    internal class Bank : Repository
    {
        public ObservableCollection<Customer> Customers;
        public List<Account> Accounts;
        private List<int> customersFolderName;

        Random rand;

        public Bank() 
        { 
            rand = new Random();

            customersFolderName = base.GetFoldersNames();

            Customers = new ObservableCollection<Customer>();
            Accounts  = new List<Account>();

            FillCustomerList();
            FillAccountList();
        }

        /// <summary>
        /// Заполнение списка клиентов
        /// </summary>
        private void FillCustomerList()
        {
            for (int i = 0; i < customersFolderName.Count; i++)
            {
                List<string> customerFile = base.FileReader(base.GetFilePath(customersFolderName[i], 0));
                string[] customerInformation = Separation(customerFile[0]);
                Customer customer = new Customer(int.Parse(customerInformation[0]), customerInformation[1], customerInformation[2], customerInformation[3]);
                Customers.Add(customer);
            }
        }

        /// <summary>
        /// Заполнение списка счетов
        /// </summary>
        private void FillAccountList()
        {
            for(int i = 0; i < customersFolderName.Count;i++)
            {
                List<string> DepositFile = base.FileReader(base.GetFilePath(customersFolderName[i], 1));
                if (DepositFile.Count > 0)
                {
                    string[] accountInformation = Separation(DepositFile[0]);
                    Accounts.Add(new Account(IntParse(accountInformation[0]), IntParse(accountInformation[1]), LongParse(accountInformation[2]), AccountType(accountInformation[3])));
                }

                List<string> NotdepositFile = base.FileReader(base.GetFilePath(customersFolderName[i], 2));
                if (NotdepositFile.Count > 0)
                {
                    string[] accountInformation = Separation(NotdepositFile[0]);
                    Accounts.Add(new Account(IntParse(accountInformation[0]), IntParse(accountInformation[1]), LongParse(accountInformation[2]), AccountType(accountInformation[3])));
                }
            }
        }

        #region Управление учётными записями и аккаунтами
        public bool CheckBalance(int number, long sum)
        {
            Account account = FindAccountByNumber(number);
            if (account.Balance < sum)
                return false;
            return true;
        }

        public void OpenTranfer(Account sender, Account recipient, long sum)
        {
            long newSenderBalance = sender.Balance -= sum;
            long newRecipientBalance = recipient.Balance += sum;

            sender.Balance = newSenderBalance;
            recipient.Balance = newRecipientBalance;

            string senderRecording = Join(sender);
            string recipientRecording = Join(recipient);

            base.DeleteFile(base.GetFilePath(sender.ID, AccountFile(sender.Type)));
            base.FileWriting(base.GetFilePath(sender.ID, AccountFile(sender.Type)), senderRecording);

            base.DeleteFile(base.GetFilePath(recipient.ID, AccountFile(sender.Type)));
            base.FileWriting(base.GetFilePath(recipient.ID, AccountFile(sender.Type)), recipientRecording);
        }

        public void AddNewAccount(Account account)
        {
            Accounts.Add(account);

            if(account.Type)
            {
                base.FileWriting(base.GetFilePath(account.ID, 1), Join(account));
            }
            else
            {
                base.FileWriting(base.GetFilePath(account.ID, 2), Join(account));
            }
        }

        public void DeleteAccount(Account account)
        {
            Accounts.Remove(account);
            base.DeleteFile(base.GetFilePath(account.ID, AccountFile(account.Type)));
        }

        public void AddNewCustomer(Customer customer, Account account)
        {
            Customers.Add(customer);
            base.CreateNewCustomerFolder(customer.ID);
            base.FileWriting(base.GetFilePath(customer.ID, 0), Join(customer));

            AddNewAccount(account);
        }

        public int CreateNewID()
        {
            return Customers.Count;
        }

        public int CreateAccountNumber()
        {
            while (true)
            {
                int accountNumber = rand.Next(100000, 1000000);

                if (UniqueAccountNumber(accountNumber))
                {
                    return accountNumber;
                }
            }
        }

        private bool UniqueAccountNumber(int accountNumber)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (accountNumber == Accounts[i].Number)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Поиск
        public Account FindAccountByID(int id, bool accountType)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].ID == id && Accounts[i].Type == accountType)
                {
                    return Accounts[i];
                }
            }
            return null;
        }
        public Account FindAccountByNumber(int number)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Number == number)
                    return Accounts[i];
            }

            return null;
        }
        #endregion

        #region Преобразование переменных
        /// <summary>
        /// Разделение строки
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string[] Separation(string text)
        {
            return text.Split('#');
        }
        private string Join(Customer customer)
        {
            string joint = customer.ID.ToString() + "#" +
                           customer.LastName + "#" +
                           customer.FirstName + "#" +
                           customer.MiddleName;
            return joint;
        }
        private string Join(Account account)
        {
            string joint = account.ID.ToString() + "#" +
                           account.Number.ToString() + "#" +
                           account.Balance.ToString() + "#" + 
                           StringAccountType(account.Type);
            return joint;
        }

        /// <summary>
        /// Перевод string в int
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private int IntParse(string text)
        {
            return int.Parse(text);
        }

        /// <summary>
        /// Перевот string в long
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private long LongParse(string text)
        {
            return long.Parse(text);
        }

        /// <summary>
        /// Получение типа аккаунта
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool AccountType(string text)
        {
            if(text == "1")
                return true;
            else
                return false;
        }

        private int AccountFile(bool type)
        {
            if(type)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private int AccountActionFile(bool type)
        {
            if (type)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        private string StringAccountType(bool type)
        {
            if(type)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        #endregion

        #region Расчёт процентов
        /// <summary>
        /// Начисление процентов за месяц
        /// </summary>
        /// <param name="accountType"> Тип счёта </param>
        /// <param name="balance"> Баланс </param>
        /// <returns></returns>
        public long MonthlyInterest(bool accountType, long balance)
        {
            long month = balance;

            if (accountType)
            {
                float percentages = (float)(balance * 1.12) / 12;
                month += (long)percentages;
            }

            return month;
        }

        /// <summary>
        /// Начисление процентов за год
        /// </summary>
        /// <param name="accountType"> Тип счёта </param>
        /// <param name="balance"> Баланс </param>
        /// <returns></returns>
        public long YearIntetest(bool accountType, long balance)
        {
            long year = balance;

            if (accountType)
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

        #endregion
    }
}
