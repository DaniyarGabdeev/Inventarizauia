using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inventarizauia
{
    /// <summary>
    /// Логика взаимодействия для ZauvkaWindow.xaml
    /// </summary>
    public partial class ZauvkaWindow : Window
    {
        Inventarizauia3Entities db;

        public ZauvkaWindow()
        {
            InitializeComponent();
            db = new Inventarizauia3Entities();
            dataGrid.ItemsSource = db.Zauvka.ToList();

            var seart = db.Zauvka.Select(s => s.StatusZauvka.title).Distinct().ToList();
            seart.Insert(0, "All");
            cmd1.ItemsSource = seart;
            cmd1.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window2 aw = new Window2();
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
                Zauvka selectedZauvka = (Zauvka)dataGrid.SelectedItem;
                if (selectedZauvka != null)
                {
                    Window2 aw = new Window2(selectedZauvka);
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
                Zauvka za = (Zauvka)dataGrid.SelectedItem;
                if (za != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.Zauvka.Remove(za);
                        db.SaveChanges();
                        dataGrid.ItemsSource = db.Zauvka.ToList();
                    }    
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tx1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string searchText = tx1.Text.ToLower();

                var fill = (from z in db.Zauvka
                            join s in db.Sotrudniki on z.sotrudnikiId equals s.sotrudnikiId
                            where s.fio.ToLower().Contains(searchText)
                            select z).ToList();

                dataGrid.ItemsSource = fill;

                if (string.IsNullOrWhiteSpace(tx1.Text))
                {
                    SearchTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    SearchTextBlock.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == true)
                {
                    string exportPath = saveFileDialog.FileName;

                    // Установка контекста лицензии
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (ExcelPackage excelPackage = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Participants");

                        int startRow = 1;

                        worksheet.Cells[startRow, 1].Value = "Номер заявки";
                        worksheet.Cells[startRow, 2].Value = "Фио";
                        worksheet.Cells[startRow, 3].Value = "Оборудование";
                        worksheet.Cells[startRow, 4].Value = "Место";
                        worksheet.Cells[startRow, 5].Value = "Дата";
                        worksheet.Cells[startRow, 6].Value = "Статус";

                        startRow++;

                        foreach (var item in dataGrid.Items)
                        {
                            if (item is Zauvka zauvka)
                            {
                                worksheet.Cells[startRow, 1].Value = zauvka.zauvkaId;
                                worksheet.Cells[startRow, 2].Value = zauvka.Sotrudniki.fio;
                                worksheet.Cells[startRow, 3].Value = zauvka.Oborudovanie.title;
                                worksheet.Cells[startRow, 4].Value = zauvka.MestoYstanovki.title;

                                // Преобразование даты в строку с использованием желаемого формата
                                worksheet.Cells[startRow, 5].Value = zauvka.data?.ToString("yyyy-MM-dd");

                                worksheet.Cells[startRow, 6].Value = zauvka.StatusZauvka.title;
                                startRow++;
                            }
                        }

                        excelPackage.SaveAs(new FileInfo(exportPath));
                        MessageBox.Show("Таблица успешно экспортирована в " + exportPath, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при экспорте в Excel: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"Призошла ошибка при сворачивании окна: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmd1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string st = cmd1.SelectedItem.ToString();
            if (st == "All")
            {
                dataGrid.ItemsSource = db.Zauvka.ToList();
            }
            else
            {
                var filt = db.Zauvka.Where(s => s.StatusZauvka.title == st).ToList();
                dataGrid.ItemsSource = filt;
            }
        }
    }
}
