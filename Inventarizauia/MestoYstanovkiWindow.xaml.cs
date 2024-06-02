using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inventarizauia
{
    public partial class MestoYstanovkiWindow : Window
    {
        Inventarizauia3Entities db;
        MestoYstanovki selectedMesto;

        public MestoYstanovkiWindow()
        {
            InitializeComponent();
            try
            {
                db = new Inventarizauia3Entities();
                dataGrid.ItemsSource = db.MestoYstanovki.ToList();
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
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(tx1.Text) || string.IsNullOrWhiteSpace(tx2.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Если поля не пустые, добавляем данные в базу
                MestoYstanovki my = new MestoYstanovki();
                my.title = tx1.Text;
                my.opisania = tx2.Text;
                db.MestoYstanovki.Add(my);
                db.SaveChanges();
                dataGrid.ItemsSource = db.MestoYstanovki.ToList();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedMesto = (MestoYstanovki)dataGrid.SelectedItem;
                if (selectedMesto != null)
                {
                    selectedMesto.title = tx1.Text;
                    selectedMesto.opisania = tx2.Text;
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.MestoYstanovki.ToList();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при редактировании данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MestoYstanovki my = (MestoYstanovki)dataGrid.SelectedItem;
                if (my != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.MestoYstanovki.Remove(my);
                        db.SaveChanges();
                        dataGrid.ItemsSource = db.MestoYstanovki.ToList();
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

        private void ClearTextBoxes()
        {
            tx1.Text = string.Empty;
            tx2.Text = string.Empty;
            selectedMesto = null;
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

