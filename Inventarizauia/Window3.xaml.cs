using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Inventarizauia
{
    public partial class Window3 : Window
    {
        Inventarizauia3Entities db = new Inventarizauia3Entities();
        int sotrudnikiId = -1;

        public Window3()
        {
            InitializeComponent();
        }

        public Window3(Sotrudniki sotrudniki) : this()
        {
            try
            {
                PopulateFields(sotrudniki);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при заполнении полей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateFields(Sotrudniki sotrudniki)
        {
            if (sotrudniki == null) return;

            Button1.Content = "Изменить";
            tx1.Text = sotrudniki.fio;
            tx2.Text = sotrudniki.telefon;
            tx3.Text = sotrudniki.role;
            tx4.Text = sotrudniki.login;
            tx5.Text = sotrudniki.parol;
            tx6.Text = sotrudniki.post;

            sotrudnikiId = sotrudniki.sotrudnikiId;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(tx1.Text) ||
                    string.IsNullOrWhiteSpace(tx2.Text) ||
                    string.IsNullOrWhiteSpace(tx3.Text) ||
                    string.IsNullOrWhiteSpace(tx4.Text) ||
                    string.IsNullOrWhiteSpace(tx5.Text) ||
                    string.IsNullOrWhiteSpace(tx6.Text))
                {
                    MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (sotrudnikiId == -1)
                {
                    Sotrudniki newSotrudniki = new Sotrudniki
                    {
                        fio = tx1.Text,
                        telefon = tx2.Text,
                        role = tx3.Text,
                        login = tx4.Text,
                        parol = tx5.Text,
                        post = tx6.Text
                    };
                    db.Sotrudniki.Add(newSotrudniki);
                }
                else
                {
                    Sotrudniki sotrudniki = db.Sotrudniki.First(s => s.sotrudnikiId == sotrudnikiId);
                    sotrudniki.fio = tx1.Text;
                    sotrudniki.telefon = tx2.Text;
                    sotrudniki.role = tx3.Text;
                    sotrudniki.login = tx4.Text;
                    sotrudniki.parol = tx5.Text;
                    sotrudniki.post = tx6.Text;
                }

                db.SaveChanges();
                SotrudnikiWindow mw = new SotrudnikiWindow();
                mw.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SotrudnikiWindow sotrudnikiWindow = new SotrudnikiWindow();
                sotrudnikiWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при закрытии окна: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"Произошла ошибка при минимизации окна: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
