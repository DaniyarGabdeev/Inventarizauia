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
using System.Windows.Shapes;

namespace Inventarizauia
{
    /// <summary>
    /// Логика взаимодействия для avtorizauia.xaml
    /// </summary>
    public partial class avtorizauia : Window
    {
        Inventarizauia3Entities db = new Inventarizauia3Entities();
        public avtorizauia()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var sot = db.Sotrudniki.FirstOrDefault(s => s.login == LoginBox.Text);
            if (sot == null)
            {
                MessageBox.Show("Неправильный логин");
                return;
            }
            if (PassBox.Password != sot.parol)
            {
                MessageBox.Show("Неправильный пароль");
                return;
            }
            if (sot.role == "Администратор")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Вход запрещен");
                return;
            }
            this.Close();
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try 
            {
                Close();
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ellipse_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
