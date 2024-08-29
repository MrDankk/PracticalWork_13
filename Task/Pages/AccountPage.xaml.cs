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
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        CustomerPage customerPage;

        public int accountNumber;
        bool accountType;

        public static event Action<int> DeleteAccount;

        public AccountPage(bool accountType, CustomerPage customerPage)
        {
            InitializeComponent();
            this.accountType = accountType;
            this.customerPage = customerPage;
            TransferPage.AccountPage += BackToAccountPage;
        }

        private void BackToAccountPage()
        {
            customerPage.OpenAccountPage(accountType);
        }

        private void DeleteAccountBtn(object sender, RoutedEventArgs e)
        {
            DeleteAccount?.Invoke(accountNumber);
        }

        private void OpenTransferPage(object sender, RoutedEventArgs e)
        {
            customerPage.OpenTransferPage(accountType, accountNumber);
        }
    }
}
