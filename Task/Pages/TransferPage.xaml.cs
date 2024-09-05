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
    /// Логика взаимодействия для TransferPage.xaml
    /// </summary>
    public partial class TransferPage : Page
    {
        public int Number;

        /// <summary>
        /// Собитие открытия страницы аккаунта
        /// </summary>
        public static Action AccountPage;

        /// <summary>
        /// событие открытия страницы переводов
        /// </summary>
        public static Action<int,int,long> Transfer;

        public TransferPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка нажатия кнопки открытия страницы аккаунта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountInfoPage(object sender, RoutedEventArgs e)
        {
            AccountPage?.Invoke();
        }

        /// <summary>
        /// Обработка нажатия кнопки открытия страницы переводов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTransfer(object sender, RoutedEventArgs e)
        {
            int recipient = int.Parse(AccountNumber.Text);
            long sum = long.Parse(Sum.Text);
            Transfer?.Invoke(Number,recipient,sum);
            AccountPage?.Invoke();
        }
    }
}
