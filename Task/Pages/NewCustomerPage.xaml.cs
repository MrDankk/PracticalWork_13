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
    /// Логика взаимодействия для NewCustomerPage.xaml
    /// </summary>
    public partial class NewCustomerPage : Page
    {
        MainWindow mainWindow;

        /// <summary>
        /// Событие создания нового клиента
        /// </summary>
        public static event Action<string, string, string, long, bool> NewCustomer;

        bool accountType;

        public NewCustomerPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Вернуться в главное меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToMainPage(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenMainPage();
        }

        /// <summary>
        /// Выбор депозитного счёта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepositRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            accountType = true;
        }

        /// <summary>
        /// Выбор недепозитного счёта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotDepositRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            accountType = false;
        }

        /// <summary>
        /// Обработка нажатия кнопки создания нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewCustomer(object sender, RoutedEventArgs e)
        {
            long balance = long.Parse(Deposit.Text);
            NewCustomer?.Invoke(FirstName.Text, LastName.Text, MiddleName.Text, balance, accountType);
            mainWindow.OpenMainPage();
        }
    }
}
