using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainPage mainPage;
        public NewCustomerPage newCustomerPage;
        public CustomerPage customerPage;
        ActionNotifications actionNotifications;

        Bank bank;

        Customer customer;
        Account deposit;
        Account notDeposit;

        /// <summary>
        /// Событие перевода средств
        /// </summary>
        public event Action<string,string,string,int,int> TransferAction;

        /// <summary>
        /// Событие создания аккаунта
        /// </summary>
        public event Action<string, string, int> AccountAction;

        /// <summary>
        /// Событие удаления аккаунта
        /// </summary>
        public event Action<string, string, int> DeleteAccountAction;

        public MainWindow()
        {
            InitializeComponent();

            bank = new Bank();

            mainPage = new MainPage(this);
            newCustomerPage = new NewCustomerPage(this);
            customerPage = new CustomerPage(this);
            actionNotifications = new ActionNotifications(this);
            OpenMainPage();

            mainPage.CustomersView.ItemsSource = bank.Customers;

            AccountPage.DeleteAccount += DeleteAccount;
            NewCustomerPage.NewCustomer += NewCustomer;
            NewAccountPage.NewAccount += NewAccount;
            TransferPage.Transfer += CheckTransfer;
        }

        #region Выбор клиента и заполнение страниц

        /// <summary>
        /// Обработка выбора клиента
        /// </summary>
        public void Select()
        {
            customer = mainPage.CustomersView.SelectedItem as Customer;
            deposit = bank.FindAccountByID(customer.ID, true);
            notDeposit = bank.FindAccountByID(customer.ID, false);
            FillCustomerPage();
        }

        /// <summary>
        /// Заполнение страницы клиента
        /// </summary>
        private void FillCustomerPage()
        {
            customerPage.CustomerName.Text = customer.Name;

            if(deposit != null)
            {
                customerPage.DepositAccountFrame.Content = customerPage.depositAccountPage;

                customerPage.depositAccountPage.accountNumber = deposit.Number;

                customerPage.depositAccountPage.AccountNumber.Text = deposit.Number.ToString();
                customerPage.depositAccountPage.AccountBalance.Text = deposit.Balance.ToString();
                customerPage.depositAccountPage.NextMonth.Text = deposit.Balance.MonthlyInterest(deposit.Type).ToString();
                customerPage.depositAccountPage.NextYear.Text = deposit.Balance.AnnualInterest(deposit.Type).ToString();

                customerPage.depositTransferPage.Balance.Text = deposit.Balance.ToString();
            }
            else
            {
                customerPage.DepositAccountFrame.Content = customerPage.newDepositAccountPage;
            }

            if(notDeposit != null)
            {
                customerPage.NotDepositAccountFrame.Content = customerPage.notDepositAccountPage;

                customerPage.notDepositAccountPage.accountNumber = notDeposit.Number;

                customerPage.notDepositAccountPage.AccountNumber.Text = notDeposit.Number.ToString();
                customerPage.notDepositAccountPage.AccountBalance.Text = notDeposit.Balance.ToString();
                customerPage.notDepositAccountPage.NextMonth.Text = notDeposit.Balance.MonthlyInterest(notDeposit.Type).ToString();
                customerPage.notDepositAccountPage.NextYear.Text = notDeposit.Balance.AnnualInterest(notDeposit.Type).ToString();
                customerPage.notDepositTransferPage.Balance.Text = notDeposit.Balance.ToString();

            }
            else
            {
                customerPage.NotDepositAccountFrame.Content = customerPage.newNotDepositAccountPage;
            }
        }

        #endregion

        #region Управление аккаунтами и учетными записями

        /// <summary>
        /// Удаление аккаунта
        /// </summary>
        /// <param name="number">Номер аккаунта</param>
        private void DeleteAccount(int number)
        {
            Account deleteAccount = bank.FindAccountByNumber(number);
            bank.DeleteAccount(deleteAccount);
            Select();
            DeleteAccountAction?.Invoke(deleteAccount.Number.ToString(), deleteAccount.Balance.ToString(), deleteAccount.ID);
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="balance">Баланс счёта</param>
        /// <param name="accountType">Тип счёта</param>
        private void NewCustomer(string firstName, string lastName, string middleName, long balance, bool accountType)
        {
            Customer newCustomer = new Customer(bank.Customers.Count, firstName, lastName, middleName);
            Account newAccount = new Account(newCustomer.ID, bank.CreateAccountNumber(), balance, accountType);
            bank.AddNewCustomer(newCustomer, newAccount);
        }

        /// <summary>
        /// Добавление нового аккаунта
        /// </summary>
        /// <param name="type">Тип аккаунта</param>
        /// <param name="balance">Баланс</param>
        private void NewAccount(bool type, long balance)
        {
            Account newAccount = new Account(customer.ID, bank.CreateAccountNumber(), balance, type);
            bank.AddNewAccount(newAccount);
            Select();
            AccountAction?.Invoke(newAccount.Number.ToString(), newAccount.Balance.ToString(), newAccount.ID);
        }

        /// <summary>
        /// Проверка и открытие перевода средств
        /// </summary>
        /// <param name="sender"> Номер отправителя </param>
        /// <param name="recipient"> Номер получателя </param>
        /// <param name="sum"> Сумма </param>
        private void CheckTransfer(int sender, int recipient, long sum)
        {
            Account senderAccount = bank.FindAccountByNumber(sender);
            Account recipientAccount = bank.FindAccountByNumber(recipient);

            if (bank.CheckBalance(sender,sum))
            {
                try
                {
                    if(recipientAccount == null || senderAccount == null) throw new TransferException("Не найден счёт");

                    bank.OpenTranfer(senderAccount, recipientAccount, sum);
                    FillCustomerPage();
                    TransferAction?.Invoke(senderAccount.Number.ToString(), recipientAccount.Number.ToString(), sum.ToString(), senderAccount.ID, recipientAccount.ID);
                }
                catch(TransferException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Недостаточно средств");
            }

        }
        #endregion

        #region Переход между страницами

        /// <summary>
        /// Открыть главную страницу
        /// </summary>
        public void OpenMainPage()
        {
            MainFrame.Content = mainPage;
        }

        /// <summary>
        /// Открыть страницу добавления нового клиента
        /// </summary>
        public void OpenNewCustomerPage()
        {
            MainFrame.Content = newCustomerPage;
        }

        /// <summary>
        /// Открыть страницу иформации о клиенте
        /// </summary>
        public void OpenCustomerPage()
        {
            try
            {
                if(customer == null) throw new CustomerException("Выберите клиента");

                MainFrame.Content = customerPage;
                customerPage.customerID = customer.ID;
            }
            catch (CustomerException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
