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
    public partial class OborudovanieWindow : Window
    {
        Inventarizauia3Entities db;

        public OborudovanieWindow()
        {
            InitializeComponent();
            try
            {
                db = new Inventarizauia3Entities();
                dataGrid.ItemsSource = db.Oborudovanie.ToList();
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
                Window1 aw = new Window1();
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
                Oborudovanie selectedOborudovanie = (Oborudovanie)dataGrid.SelectedItem;
                if (selectedOborudovanie != null)
                {
                    Window1 aw = new Window1(selectedOborudovanie);
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
                Oborudovanie ob = (Oborudovanie)dataGrid.SelectedItem;
                if (ob != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.Oborudovanie.Remove(ob);
                        db.SaveChanges();
                        dataGrid.ItemsSource = db.Oborudovanie.ToList();
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
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Оборудование");

                        int startRow = 1;

                        worksheet.Cells[startRow, 1].Value = "Название";
                        worksheet.Cells[startRow, 2].Value = "Дата";
                        worksheet.Cells[startRow, 3].Value = "Сотрудник";
                        worksheet.Cells[startRow, 4].Value = "Производитель";
                        worksheet.Cells[startRow, 5].Value = "Тип оборудования";
                        worksheet.Cells[startRow, 6].Value = "Место";
                        worksheet.Cells[startRow, 7].Value = "Статус";

                        startRow++;

                        foreach (var item in dataGrid.Items)
                        {
                            if (item is Oborudovanie oborudovanie)
                            {
                                worksheet.Cells[startRow, 1].Value = oborudovanie.title;
                                worksheet.Cells[startRow, 2].Value = oborudovanie.dataPokypki?.ToString("yyyy-MM-dd");
                                worksheet.Cells[startRow, 3].Value = oborudovanie.Sotrudniki.fio;
                                worksheet.Cells[startRow, 4].Value = oborudovanie.Proizvoditel.title;
                                worksheet.Cells[startRow, 5].Value = oborudovanie.TipEquipment.title;
                                worksheet.Cells[startRow, 6].Value = oborudovanie.MestoYstanovki.title;
                                worksheet.Cells[startRow, 7].Value = oborudovanie.StatusOborudovania.title;
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
                MessageBox.Show($"Произошла ошибка при экспорте данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void tx1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string searchText = tx1.Text.ToLower();

                var fill = (from z in db.Oborudovanie
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
    }
}
