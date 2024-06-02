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
    /// Логика взаимодействия для TipEquipmentWindow.xaml
    /// </summary>
    public partial class TipEquipmentWindow : Window
    {
        Inventarizauia3Entities db;
        TipEquipment selectedEquipment;

        public TipEquipmentWindow()
        {
            InitializeComponent();
            db = new Inventarizauia3Entities();
            dataGrid.ItemsSource = db.TipEquipment.ToList();
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

                if (selectedEquipment == null)
                {
                    // Добавление нового оборудования
                    TipEquipment te = new TipEquipment();
                    te.title = tx1.Text;
                    te.opisania = tx2.Text;
                    db.TipEquipment.Add(te);
                }
                else
                {
                    // Обновление существующего оборудования
                    selectedEquipment.title = tx1.Text;
                    selectedEquipment.opisania = tx2.Text;
                }

                db.SaveChanges();
                dataGrid.ItemsSource = db.TipEquipment.ToList();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            selectedEquipment = (TipEquipment)dataGrid.SelectedItem;
            if (selectedEquipment != null)
            {
                tx1.Text = selectedEquipment.title;
                tx2.Text = selectedEquipment.opisania;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            TipEquipment te = (TipEquipment)dataGrid.SelectedItem;
            if (te != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.TipEquipment.Remove(te);
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.TipEquipment.ToList();
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
            selectedEquipment = null;
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
