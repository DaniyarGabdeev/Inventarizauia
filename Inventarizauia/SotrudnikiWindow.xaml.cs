using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Inventarizauia
{
    public partial class SotrudnikiWindow : Window
    {
        Inventarizauia3Entities db;

        public SotrudnikiWindow()
        {
            InitializeComponent();
            try
            {
                db = new Inventarizauia3Entities();
                dataGrid.ItemsSource = db.Sotrudniki.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window3 aw = new Window3();
                aw.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при открытии нового окна: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sotrudniki selectedSotrudniki = (Sotrudniki)dataGrid.SelectedItem;
                if (selectedSotrudniki != null)
                {
                    Window3 aw = new Window3(selectedSotrudniki);
                    aw.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при открытии окна для редактирования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sotrudniki so = (Sotrudniki)dataGrid.SelectedItem;
                if (so != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.Sotrudniki.Remove(so);
                        db.SaveChanges();
                        dataGrid.ItemsSource = db.Sotrudniki.ToList();
                    }              
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
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

