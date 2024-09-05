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

        /// <summary>
        /// Событие удаления аккаунта
        /// </summary>
        public static event Action<int> DeleteAccount;

        public AccountPage(bool accountType, CustomerPage customerPage)
        {
            InitializeComponent();
            this.accountType = accountType;
            this.customerPage = customerPage;
            TransferPage.AccountPage += BackToAccountPage;
        }

        /// <summary>
        /// Возвращение на страницу аккаунта
        /// </summary>
        private void BackToAccountPage()
        {
            customerPage.OpenAccountPage(accountType);
        }

        /// <summary>
        /// Обработка кнопки удаления аккаунта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAccountBtn(object sender, RoutedEventArgs e)
        {
            DeleteAccount?.Invoke(accountNumber);
        }

        /// <summary>
        /// Открытие страницы перевода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTransferPage(object sender, RoutedEventArgs e)
        {
            customerPage.OpenTransferPage(accountType, accountNumber);
        }
    }
}
