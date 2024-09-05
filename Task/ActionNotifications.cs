using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using RepositoryLibrary;

namespace Task
{
    internal class ActionNotifications
    {
        MainWindow mainWindow;
        public ActionNotifications(MainWindow mainWindow) 
        {
            this.mainWindow = mainWindow;

            mainWindow.TransferAction += TransferNotification;
            mainWindow.AccountAction += NewAccountNotification;
            mainWindow.DeleteAccountAction += DeleteAccountNotification;
            mainWindow.customerPage.ActionLog += GetActionLog;
        }

        /// <summary>
        /// Уведомление о переводе денег между аккаунтами
        /// </summary>
        /// <param name="senderNumber"> Номер отправителя </param>
        /// <param name="recipientNumber"> Номер получателя </param>
        /// <param name="sum"> Сумма </param>
        /// <param name="senderID"> Индекс отпраителя </param>
        /// <param name="recipientID"> Индекс получателя </param>
        private void TransferNotification(string senderNumber,string recipientNumber, string sum, int senderID, int recipientID)
        {
            string text = $"Совершен перевод {sum}руб. \n с вашего счёта: {senderNumber} \n на счёт {recipientNumber}";

            string senderRecording = $"{DateTime.Now} - Совершен перевод {sum}руб. с вашего счёта: {senderNumber} на счёт {recipientNumber}";
            string recipientRecording = $"{DateTime.Now} - На ваш счёт {recipientNumber} поступил платёж {sum}руб.";

            string senderPath = $"Customers\\{senderID.ToString()}\\Action.txt";
            string recipientPath = $"Customers\\{recipientID.ToString()}\\Action.txt";

            Writing.FileWriting(senderPath, senderRecording);
            Writing.FileWriting(recipientPath, recipientRecording);

            MessageBox.Show(text);
        }

        /// <summary>
        /// Уведомление о новом аккаунте
        /// </summary>
        /// <param name="number"> Номер аккаунта </param>
        /// <param name="sum"> Баланс </param>
        /// <param name="id"> Индекс </param>
        private void NewAccountNotification(string number, string sum, int id)
        {
            string text = $"Открыт новый счёт {number} с балансом {sum}";
            string recording = $"{DateTime.Now} - Открыт новый счёт {number} с балансом {sum}";
            string path = $"Customers\\{id.ToString()}\\Action.txt";

            Writing.FileWriting(path, recording);

            MessageBox.Show(text);
        }

        /// <summary>
        /// Уведомление о удалении аккаунта
        /// </summary>
        /// <param name="number"> Номер </param>
        /// <param name="balance"> Баланс </param>
        /// <param name="id"> Индекс </param>
        private void DeleteAccountNotification(string number, string balance, int id)
        {
            string text = $"Закрыт счёт {number} с балансом {balance}";
            string recording = $"{DateTime.Now} - Закрыт счёт {number} с балансом {balance}";
            string path = $"Customers\\{id.ToString()}\\Action.txt";

            Writing.FileWriting(path, recording);

            MessageBox.Show(text);
        }

        /// <summary>
        /// Получение журнала уведомлений
        /// </summary>
        /// <param name="id"> Индекс клиента </param>
        private void GetActionLog(int id)
        {
            string path = $"Customers\\{id.ToString()}\\Action.txt";
            List<string> list = Reader.GetList(path);
            string actionLog = String.Join("\n",list);
            if (actionLog != null && actionLog != "")
            {
                MessageBox.Show(actionLog);
            }
            else
            {
                MessageBox.Show("Журнал действий пуст");
            }
        }
    }
}
