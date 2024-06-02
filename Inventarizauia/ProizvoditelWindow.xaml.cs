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
    /// Логика взаимодействия для ProizvoditelWindow.xaml
    /// </summary>
    public partial class ProizvoditelWindow : Window
    {
        Inventarizauia3Entities db;
        Proizvoditel selectedProizvoditel;

        public ProizvoditelWindow()
        {
            InitializeComponent();
            db = new Inventarizauia3Entities();
            dataGrid.ItemsSource = db.Proizvoditel.ToList();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(tx1.Text) || string.IsNullOrWhiteSpace(tx2.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (selectedProizvoditel == null)
                {
                    // Добавление нового производителя
                    Proizvoditel pr = new Proizvoditel();
                    pr.title = tx1.Text;
                    pr.telefon = tx2.Text;
                    db.Proizvoditel.Add(pr);
                }
                else
                {
                    // Обновление существующего производителя
                    selectedProizvoditel.title = tx1.Text;
                    selectedProizvoditel.telefon = tx2.Text;
                }

                db.SaveChanges();
                dataGrid.ItemsSource = db.Proizvoditel.ToList();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            selectedProizvoditel = (Proizvoditel)dataGrid.SelectedItem;
            if (selectedProizvoditel != null)
            {
                tx1.Text = selectedProizvoditel.title;
                tx2.Text = selectedProizvoditel.telefon;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Proizvoditel pr = (Proizvoditel)dataGrid.SelectedItem;
            if (pr != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.Proizvoditel.Remove(pr);
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.Proizvoditel.ToList();
                }
                    
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearTextBoxes()
        {
            tx1.Text = string.Empty;
            tx2.Text = string.Empty;
            selectedProizvoditel = null;
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
