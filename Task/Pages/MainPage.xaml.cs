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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MainWindow mainWindow;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Страница добавления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenNewCustomerPage(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenNewCustomerPage();
        }

        /// <summary>
        /// Страница информации о клиенте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCustomerPage(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenCustomerPage();
        }

        /// <summary>
        /// Отслеживание выбора в списке клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            mainWindow.Select();
        }
    }
}
