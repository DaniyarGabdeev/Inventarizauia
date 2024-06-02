using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Inventarizauia
{
    public partial class Window1 : Window
    {
        Inventarizauia3Entities db = new Inventarizauia3Entities();
        int oborudovanieId = -1;

        public Window1()
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

        public Window1(Oborudovanie oborudovanie) : this()
        {
            try
            {
                PopulateFields(oborudovanie);
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
                foreach (Proizvoditel tool in db.Proizvoditel.ToList())
                {
                    cmb2.Items.Add(tool.title);
                }
                foreach (TipEquipment client in db.TipEquipment.ToList())
                {
                    cmb3.Items.Add(client.title);
                }
                foreach (MestoYstanovki status in db.MestoYstanovki.ToList())
                {
                    cmb4.Items.Add(status.title);
                }
                foreach (StatusOborudovania tool in db.StatusOborudovania.ToList())
                {
                    cmb5.Items.Add(tool.title);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при заполнении комбо-боксов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateFields(Oborudovanie oborudovanie)
        {
            if (oborudovanie == null) return;

            Button1.Content = "Изменить";
            tx1.Text = oborudovanie.title;
            dpMainDate.SelectedDate = oborudovanie.dataPokypki;

            cmb1.SelectedItem = oborudovanie.Sotrudniki.fio;
            cmb2.SelectedItem = oborudovanie.Proizvoditel.title;
            cmb3.SelectedItem = oborudovanie.TipEquipment.title;
            cmb4.SelectedItem = oborudovanie.MestoYstanovki.title;
            cmb5.SelectedItem = oborudovanie.StatusOborudovania.title;

            oborudovanieId = oborudovanie.oborudovanieId;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на пустые поля
                if (cmb1.SelectedValue == null || cmb2.SelectedValue == null || cmb3.SelectedValue == null || cmb4.SelectedValue == null || cmb5.SelectedValue == null || string.IsNullOrWhiteSpace(tx1.Text) || dpMainDate.SelectedDate == null)
                {
                    MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Sotrudniki sotrudniki = db.Sotrudniki.FirstOrDefault(so => so.fio == cmb1.SelectedValue.ToString());
                Proizvoditel proizvoditel = db.Proizvoditel.FirstOrDefault(pr => pr.title == cmb2.SelectedValue.ToString());
                TipEquipment tipEquipment = db.TipEquipment.FirstOrDefault(te => te.title == cmb3.SelectedValue.ToString());
                MestoYstanovki mestoYstanovki = db.MestoYstanovki.FirstOrDefault(my => my.title == cmb4.SelectedValue.ToString());
                StatusOborudovania statusOborudovania = db.StatusOborudovania.FirstOrDefault(st => st.title == cmb5.SelectedValue.ToString());

                if (sotrudniki == null || proizvoditel == null || tipEquipment == null || mestoYstanovki == null || statusOborudovania == null)
                {
                    MessageBox.Show("Не удалось найти выбранные значения в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (oborudovanieId == -1)
                {
                    Oborudovanie newOborudovanie = new Oborudovanie
                    {
                        title = tx1.Text,
                        dataPokypki = (dpMainDate.SelectedDate ?? DateTime.MinValue).Date,
                        sotrudnikiId = sotrudniki.sotrudnikiId,
                        proizvoditelId = proizvoditel.proizvoditelId,
                        tipEquipmentId = tipEquipment.tipEquipmentId,
                        mestoYstanovkiId = mestoYstanovki.mestoYstanovkiId,
                        statusOborudovaniaId = statusOborudovania.statusOborudovaniaId
                    };
                    db.Oborudovanie.Add(newOborudovanie);
                }
                else
                {
                    Oborudovanie oborudovanie = db.Oborudovanie.First(o => o.oborudovanieId == oborudovanieId);
                    oborudovanie.title = tx1.Text;
                    oborudovanie.dataPokypki = (dpMainDate.SelectedDate ?? DateTime.MinValue).Date;
                    oborudovanie.sotrudnikiId = sotrudniki.sotrudnikiId;
                    oborudovanie.proizvoditelId = proizvoditel.proizvoditelId;
                    oborudovanie.tipEquipmentId = tipEquipment.tipEquipmentId;
                    oborudovanie.mestoYstanovkiId = mestoYstanovki.mestoYstanovkiId;
                    oborudovanie.statusOborudovaniaId = statusOborudovania.statusOborudovaniaId;
                }

                db.SaveChanges();
                OborudovanieWindow mw = new OborudovanieWindow();
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
                OborudovanieWindow oborudovanie = new OborudovanieWindow();
                oborudovanie.Show();
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

