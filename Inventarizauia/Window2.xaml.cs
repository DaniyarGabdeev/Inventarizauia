using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Inventarizauia
{
    public partial class Window2 : Window
    {
        int appID = -1;
        Inventarizauia3Entities db = new Inventarizauia3Entities();

        public Window2()
        {
            InitializeComponent();
            try
            {
                PopulateComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Window2(Zauvka app) : this()
        {
            try
            {
                PopulateFields(app);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при заполнении полей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateComboBoxes()
        {
            try
            {
                foreach (Sotrudniki status in db.Sotrudniki.ToList())
                {
                    cmb1.Items.Add(status.fio);
                }
                foreach (Oborudovanie tool in db.Oborudovanie.ToList())
                {
                    cmb2.Items.Add(tool.title);
                }
                foreach (MestoYstanovki status in db.MestoYstanovki.ToList())
                {
                    cmb3.Items.Add(status.title);
                }
                foreach (StatusZauvka tool in db.StatusZauvka.ToList())
                {
                    cmb4.Items.Add(tool.title);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при заполнении комбо-боксов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateFields(Zauvka app)
        {
            if (app == null) return;

            Button1.Content = "Изменить";

            cmb1.SelectedItem = app.Sotrudniki.fio;
            cmb2.SelectedItem = app.Oborudovanie.title;
            cmb3.SelectedItem = app.MestoYstanovki.title;
            cmb4.SelectedItem = app.StatusZauvka.title;

            dpMainDate.SelectedDate = app.data;
            appID = app.zauvkaId;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на пустые поля
                if (cmb1.SelectedValue == null || cmb2.SelectedValue == null || cmb3.SelectedValue == null || cmb4.SelectedValue == null || dpMainDate.SelectedDate == null)
                {
                    MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Sotrudniki sotrudniki = db.Sotrudniki.FirstOrDefault(so => so.fio == cmb1.SelectedValue.ToString());
                Oborudovanie oborudovanie = db.Oborudovanie.FirstOrDefault(ob => ob.title == cmb2.SelectedValue.ToString());
                MestoYstanovki mestoYstanovki = db.MestoYstanovki.FirstOrDefault(my => my.title == cmb3.SelectedValue.ToString());
                StatusZauvka statusZauvka = db.StatusZauvka.FirstOrDefault(sz => sz.title == cmb4.SelectedValue.ToString());

                if (sotrudniki == null || oborudovanie == null || mestoYstanovki == null || statusZauvka == null)
                {
                    MessageBox.Show("Не удалось найти выбранные значения в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (appID == -1)
                {
                    Zauvka newZauvka = new Zauvka
                    {
                        sotrudnikiId = sotrudniki.sotrudnikiId,
                        oborudovanieId = oborudovanie.oborudovanieId,
                        mestoYstanovkiId = mestoYstanovki.mestoYstanovkiId,
                        data = (dpMainDate.SelectedDate ?? DateTime.MinValue).Date,
                        statusZauvkaId = statusZauvka.statusZauvkaId
                    };
                    db.Zauvka.Add(newZauvka);
                }
                else
                {
                    Zauvka zauvka = db.Zauvka.First(a => a.zauvkaId == appID);
                    zauvka.sotrudnikiId = sotrudniki.sotrudnikiId;
                    zauvka.oborudovanieId = oborudovanie.oborudovanieId;
                    zauvka.mestoYstanovkiId = mestoYstanovki.mestoYstanovkiId;
                    zauvka.data = (dpMainDate.SelectedDate ?? DateTime.MinValue).Date;
                    zauvka.statusZauvkaId = statusZauvka.statusZauvkaId;
                }

                db.SaveChanges();
                ZauvkaWindow mw = new ZauvkaWindow();
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
                ZauvkaWindow zauvkaWindow = new ZauvkaWindow();
                zauvkaWindow.Show();
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
