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

namespace Task
{
    internal class ActionNotifications : Repository
    {
        public ActionNotifications() 
        {
            MainWindow.TransferAction += TransferNotification;
            MainWindow.AccountAction += NewAccountNotification;
            MainWindow.DeleteAccountAction += DeleteAccountNotification;
            CustomerPage.ActionLog += GetActionLog;
        }

        private void TransferNotification(string senderNumber,string recipientNumber, string sum, int senderID, int recipientID)
        {
            string text = $"Совершен перевод {sum}руб. \n с вашего счёта: {senderNumber} \n на счёт {recipientNumber}";

            string senderRecording = $"{DateTime.Now} - Совершен перевод {sum}руб. с вашего счёта: {senderNumber} на счёт {recipientNumber}";
            string recipientRecording = $"{DateTime.Now} - На ваш счёт {recipientNumber} поступил платёж {sum}руб.";

            string senderPath = $"Customers\\{senderID.ToString()}\\Action.txt";
            string recipientPath = $"Customers\\{recipientID.ToString()}\\Action.txt";

            base.FileWriting(senderPath, senderRecording);
            base.FileWriting(recipientPath, recipientRecording);

            MessageBox.Show(text);
        }

        private void NewAccountNotification(string number, string sum, int id)
        {
            string text = $"Открыт новый счёт {number} с балансом {sum}";
            string recording = $"{DateTime.Now} - Открыт новый счёт {number} с балансом {sum}";
            string path = $"Customers\\{id.ToString()}\\Action.txt";

            base.FileWriting(path, recording);

            MessageBox.Show(text);
        }

        private void DeleteAccountNotification(string number, string balance, int id)
        {
            string text = $"Закрыт счёт {number} с балансом {balance}";
            string recording = $"{DateTime.Now} - Закрыт счёт {number} с балансом {balance}";
            string path = $"Customers\\{id.ToString()}\\Action.txt";

            base.FileWriting(path, recording);

            MessageBox.Show(text);
        }

        private void GetActionLog(int id)
        {
            string path = $"Customers\\{id.ToString()}\\Action.txt";
            List<string> list = base.FileReader(path);
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
