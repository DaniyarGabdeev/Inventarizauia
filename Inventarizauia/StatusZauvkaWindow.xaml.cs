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
    /// Логика взаимодействия для StatusZauvkaWindow.xaml
    /// </summary>
    using System.Windows;

    public partial class StatusZauvkaWindow : Window
    {
        Inventarizauia3Entities db;
        StatusZauvka selectedStatus;

        public StatusZauvkaWindow()
        {
            InitializeComponent();
            db = new Inventarizauia3Entities();
            dataGrid.ItemsSource = db.StatusZauvka.ToList();
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на пустое поле
                if (string.IsNullOrWhiteSpace(tx1.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поле.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (selectedStatus == null)
                {
                    // Добавление нового статуса заявки
                    StatusZauvka sz = new StatusZauvka();
                    sz.title = tx1.Text;
                    db.StatusZauvka.Add(sz);
                }
                else
                {
                    // Обновление существующего статуса заявки
                    selectedStatus.title = tx1.Text;
                }

                db.SaveChanges();
                dataGrid.ItemsSource = db.StatusZauvka.ToList();
                ClearTextBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            selectedStatus = (StatusZauvka)dataGrid.SelectedItem;
            if (selectedStatus != null)
            {
                tx1.Text = selectedStatus.title;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            StatusZauvka sz = (StatusZauvka)dataGrid.SelectedItem;
            if (sz != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.StatusZauvka.Remove(sz);
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.StatusZauvka.ToList();
                }
                    
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearTextBox()
        {
            tx1.Text = string.Empty;
            selectedStatus = null;
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
