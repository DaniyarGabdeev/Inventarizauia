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
    /// Логика взаимодействия для StatusOborudovaniaWindow.xaml
    /// </summary>
    public partial class StatusOborudovaniaWindow : Window
    {
        Inventarizauia3Entities db;
        StatusOborudovania selectedStatus;

        public StatusOborudovaniaWindow()
        {
            InitializeComponent();
            db = new Inventarizauia3Entities();
            dataGrid.ItemsSource = db.StatusOborudovania.ToList();
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
                    // Добавление нового статуса оборудования
                    StatusOborudovania st = new StatusOborudovania();
                    st.title = tx1.Text;
                    db.StatusOborudovania.Add(st);
                }
                else
                {
                    // Обновление существующего статуса оборудования
                    selectedStatus.title = tx1.Text;
                }

                db.SaveChanges();
                dataGrid.ItemsSource = db.StatusOborudovania.ToList();
                ClearTextBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            selectedStatus = (StatusOborudovania)dataGrid.SelectedItem;
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
            StatusOborudovania st = (StatusOborudovania)dataGrid.SelectedItem;
            if (st != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.StatusOborudovania.Remove(st);
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.StatusOborudovania.ToList();
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
