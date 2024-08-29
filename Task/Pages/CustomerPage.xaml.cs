using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        MainWindow mainWindow;

        public AccountPage depositAccountPage;
        public AccountPage notDepositAccountPage;

        public NewAccountPage newDepositAccountPage;
        public NewAccountPage newNotDepositAccountPage;

        public TransferPage depositTransferPage;
        public TransferPage notDepositTransferPage;

        public int customerID;

        public static event Action<int> ActionLog;

        public CustomerPage(MainWindow mainWindow)
        { 
            InitializeComponent();
            this.mainWindow = mainWindow;

            depositAccountPage = new AccountPage(true, this);
            depositAccountPage.AccountType.Text = "Депозитный счёт";
            notDepositAccountPage = new AccountPage(false, this);
            notDepositAccountPage.AccountType.Text = "Недепозитный счёт";

            newDepositAccountPage = new NewAccountPage(true);
            newDepositAccountPage.AccountType.Text = "Депозитный счёт";
            newNotDepositAccountPage = new NewAccountPage(false);
            newNotDepositAccountPage.AccountType.Text = "Недепозитный счёт";

            depositTransferPage = new TransferPage();
            notDepositTransferPage = new TransferPage();
        }

        public void OpenTransferPage(bool type, int number)
        {
            if (type)
            {
                DepositAccountFrame.Content = depositTransferPage;
                depositTransferPage.Number = number;
            }
            else
            {
                NotDepositAccountFrame.Content = notDepositTransferPage;
                notDepositTransferPage.Number = number;
            }
        }

        public void OpenAccountPage(bool type)
        {
            if (type)
            {
                DepositAccountFrame.Content = depositAccountPage;
            }
            else
            {
                NotDepositAccountFrame.Content = notDepositAccountPage;
            }
        }

        private void BackToMainMenuPage(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenMainPage();
            ClearInputs();
        }

        private void ClearInputs()
        {
            newDepositAccountPage.AccountBalance.Text = "";
            newNotDepositAccountPage.AccountBalance.Text = "";
        }

        private void GetActionLog(object sender, RoutedEventArgs e)
        {
            ActionLog?.Invoke(customerID);
        }
    }
}
