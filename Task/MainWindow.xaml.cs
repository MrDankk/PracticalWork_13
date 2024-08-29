using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static event Action<string,string,string,int,int> TransferAction;
        public static event Action<string, string, int> AccountAction;
        public static event Action<string, string, int> DeleteAccountAction;

        public MainWindow()
        {
            InitializeComponent();

            bank = new Bank();

            mainPage = new MainPage(this);
            newCustomerPage = new NewCustomerPage(this);
            customerPage = new CustomerPage(this);
            OpenMainPage();

            actionNotifications = new ActionNotifications();

            mainPage.CustomersView.ItemsSource = bank.Customers;

            AccountPage.DeleteAccount += DeleteAccount;
            NewCustomerPage.NewCustomer += NewCustomer;
            NewAccountPage.NewAccount += NewAccount;
            TransferPage.Transfer += CheckTransfer;
        }

        #region Выбор клиента и заполнение страниц
        public void Select()
        {
            customer = mainPage.CustomersView.SelectedItem as Customer;
            deposit = bank.FindAccountByID(customer.ID, true);
            notDeposit = bank.FindAccountByID(customer.ID, false);
            FillCustomerPage();
        }
        private void FillCustomerPage()
        {
            customerPage.CustomerName.Text = customer.Name;

            if (deposit != null)
            {
                customerPage.DepositAccountFrame.Content = customerPage.depositAccountPage;

                customerPage.depositAccountPage.accountNumber = deposit.Number;

                customerPage.depositAccountPage.AccountNumber.Text = deposit.Number.ToString();
                customerPage.depositAccountPage.AccountBalance.Text = deposit.Balance.ToString();
                customerPage.depositAccountPage.NextMonth.Text = bank.MonthlyInterest(deposit.Type,deposit.Balance).ToString();
                customerPage.depositAccountPage.NextYear.Text = bank.YearIntetest(deposit.Type,deposit.Balance).ToString();
                customerPage.depositTransferPage.Balance.Text = deposit.Balance.ToString();
            }
            else
            {
                customerPage.DepositAccountFrame.Content = customerPage.newDepositAccountPage;
            }

            if (notDeposit != null)
            {
                customerPage.NotDepositAccountFrame.Content = customerPage.notDepositAccountPage;

                customerPage.notDepositAccountPage.accountNumber = notDeposit.Number;

                customerPage.notDepositAccountPage.AccountNumber.Text = notDeposit.Number.ToString();
                customerPage.notDepositAccountPage.AccountBalance.Text = notDeposit.Balance.ToString();
                customerPage.notDepositAccountPage.NextMonth.Text = bank.MonthlyInterest(notDeposit.Type, notDeposit.Balance).ToString();
                customerPage.notDepositAccountPage.NextYear.Text = bank.YearIntetest(notDeposit.Type, notDeposit.Balance).ToString();
                customerPage.notDepositTransferPage.Balance.Text = notDeposit.Balance.ToString();

            }
            else
            {
                customerPage.NotDepositAccountFrame.Content = customerPage.newNotDepositAccountPage;
            }
        }

        #endregion

        #region Управление аккаунтами и учетными записями
        private void NewAccount(bool type, long balance)
        {
            Account newAccount = new Account(customer.ID, bank.CreateAccountNumber(), balance, type);
            bank.AddNewAccount(newAccount);
            Select();
            AccountAction?.Invoke(newAccount.Number.ToString(), newAccount.Balance.ToString(), newAccount.ID);
        }
        private void DeleteAccount(int number)
        {
            Account deleteAccount = bank.FindAccountByNumber(number);
            bank.DeleteAccount(deleteAccount);
            Select();
            DeleteAccountAction?.Invoke(deleteAccount.Number.ToString(), deleteAccount.Balance.ToString(), deleteAccount.ID);
        }
        private void NewCustomer(string firstName, string lastName, string middleName, long balance, bool accountType)
        {
            Customer newCustomer = new Customer(bank.CreateNewID(), firstName, lastName, middleName);
            Account newAccount = new Account(newCustomer.ID, bank.CreateAccountNumber(), balance, accountType);
            bank.AddNewCustomer(newCustomer, newAccount);
        }

        private void CheckTransfer(int sender, int recipient, long sum)
        {
            Account senderAccount = bank.FindAccountByNumber(sender);
            Account recipientAccount = bank.FindAccountByNumber(recipient);

            if (bank.CheckBalance(sender,sum))
            {
                if (recipientAccount != null)
                {
                    bank.OpenTranfer(senderAccount, recipientAccount, sum);

                    FillCustomerPage();
                }
                else
                {
                    MessageBox.Show("Счёт не найден");
                }
            }
            else
            {
                MessageBox.Show("Недостаточно средств");
            }

            TransferAction?.Invoke(senderAccount.Number.ToString(),recipientAccount.Number.ToString(),sum.ToString(), senderAccount.ID, recipientAccount.ID);
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
            if(customer != null)
            { 
                MainFrame.Content = customerPage;
                customerPage.customerID = customer.ID;
            }
        }
        #endregion
    }
}
