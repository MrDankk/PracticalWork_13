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
    /// Логика взаимодействия для NewAccountPage.xaml
    /// </summary>
    public partial class NewAccountPage : Page
    {
        bool accountType;

        /// <summary>
        /// Событие создания нового аккаунта
        /// </summary>
        public static Action<bool, long> NewAccount;

        public NewAccountPage(bool accountType)
        {
            InitializeComponent();
            this.accountType = accountType;
        }

        /// <summary>
        /// Обработка нажатия кнопки создания нового аккаунта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewAccount(object sender, RoutedEventArgs e)
        {
            long balance = long.Parse(AccountBalance.Text);
            NewAccount?.Invoke(accountType, balance);
        }
    }
}
