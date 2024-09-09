using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using RepositoryLibrary;

namespace Task
{
    internal class Bank
    {
        public ObservableCollection <Customer> Customers;
        public ObservableCollection <Account> Accounts;
        private List<string[]> customersFolderName;

        Random rand;

        public Bank() 
        { 
            rand = new Random();

            Create.Folder("Customers");
            customersFolderName = Reader.GetFoldersNames("Customers\\");

            Customers = new ObservableCollection<Customer>();
            Accounts  = new ObservableCollection<Account>();

            FillCustomerList();
            FillAccountList();
        }

        #region Управление учётными записями и аккаунтами

        /// <summary>
        /// Проверка баланса
        /// </summary>
        /// <param name="number"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public bool CheckBalance(int number, long sum)
        {
            Account account = FindAccountByNumber(number);
            if (account.Balance < sum)
                return false;
            return true;
        }

        /// <summary>
        /// Перевод между счетами
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="recipient"> Получатель </param>
        /// <param name="sum"> Сумма </param>
        public void OpenTranfer(Account sender, Account recipient, long sum)
        {
            sender -= sum;
            recipient += sum;

            string senderRecording = Connect(sender);
            string recipientRecording = Connect(recipient);

            string senderPath = sender.Type.AccountPath(sender.ID, customersFolderName);
            string recipientPath = recipient.Type.AccountPath(recipient.ID, customersFolderName);

            Delete.File(senderPath);
            Writing.FileWriting(senderPath, senderRecording);

            Delete.File(recipientPath);
            Writing.FileWriting(recipientPath, recipientRecording);
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="account"></param>
        public void AddNewCustomer(Customer customer, Account account)
        {
            customer.AddTo(Customers);
            string path = customer.ID.NewCustomerPath();

            Create.Folder($"Customers\\{customer.ID}");
            Writing.FileWriting(path ,Connect(customer));

            AddNewAccount(account);

            Create.CreateAllFile(customer.ID);
            AddNewCustomerFolderNames(customer.ID);
        }

        /// <summary>
        /// Добавление нового аккаунта
        /// </summary>
        /// <param name="account"></param>
        public void AddNewAccount(Account account)
        {
            Accounts.Add(account);

            string path = account.Type.NewAccountPath(account.ID);

            Writing.FileWriting(path, Connect(account));
        }

        /// <summary>
        /// Удаление аккаунта
        /// </summary>
        /// <param name="account"></param>
        public void DeleteAccount(Account account)
        {
            string path = account.Type.AccountPath(account.ID, customersFolderName);
            Delete.File(path);

            account.DeleteFrom(Accounts);
        }

        /// <summary>
        /// Создание нового номера
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///  Проверка уникальности номера
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Добавление путей к файлам нового клиента
        /// </summary>
        /// <param name="id"></param>
        private void AddNewCustomerFolderNames(int id)
        {
            string[] customerFolder = new string[4];

            customerFolder[0] = $"Customers\\{id}\\N_Account.txt";
            customerFolder[1] = $"Customers\\{id}\\D_Account.txt";
            customerFolder[2] = $"Customers\\{id}\\Customer.txt";
            customerFolder[3] = $"Customers\\{id}\\Action.txt";

            customerFolder.AddTo(customersFolderName);
        }
        #endregion

        #region Поиск

        /// <summary>
        /// Поиск аккаунта по индексу
        /// </summary>
        /// <param name="id"> Индекс </param>
        /// <param name="accountType"> Тип аккаунта </param>
        /// <returns></returns>
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

        /// <summary>
        /// Поиск аккаунта по номеру
        /// </summary>
        /// <param name="number"> Номер </param>
        /// <returns></returns>
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

        #region Соединение для записи

        private string Connect(Customer customer)
        {
            string[] arr = customer.CustomerArray();

            return arr.Join();
        }
        private string Connect(Account account)
        {
            string[] arr = account.AccountArray();

            return arr.Join();
        }

        #endregion

        #region Стартовая проверка списка клиентов и счетов

        /// <summary>
        /// Заполнение списка клиентов
        /// </summary>
        private void FillCustomerList()
        {
            for (int i = 0; i < customersFolderName.Count; i++)
            {
                string path = i.CustomerPath(customersFolderName);
                List<string> customerFile = Reader.GetList(path);
                string[] customerInformation = customerFile[0].Separation();
                Customer customer = new Customer(int.Parse(customerInformation[0]), customerInformation[1], customerInformation[2], customerInformation[3]);
                Customers.Add(customer);
            }
        }

        /// <summary>
        /// Заполнение списка счетов
        /// </summary>
        private void FillAccountList()
        {
            for (int i = 0; i < customersFolderName.Count; i++)
            {
                string depositPath = true.AccountPath(i, customersFolderName);
                List<string> DepositFile = Reader.GetList(depositPath);
                if (DepositFile.Count > 0)
                {
                    string[] accountInformation = DepositFile[0].Separation();
                    Accounts.Add(new Account(accountInformation[0].IntParse(), accountInformation[1].IntParse(), accountInformation[2].LongParse(), accountInformation[3].BoolType()));
                }

                string notdepositPath = false.AccountPath(i, customersFolderName);
                List<string> NotdepositFile = Reader.GetList(notdepositPath);
                if (NotdepositFile.Count > 0)
                {
                    string[] accountInformation = NotdepositFile[0].Separation();
                    Accounts.Add(new Account(accountInformation[0].IntParse(), accountInformation[1].IntParse(), accountInformation[2].LongParse(), accountInformation[3].BoolType()));
                }
            }
        }

        #endregion
    }
}
