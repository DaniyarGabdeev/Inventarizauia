using System;
using System.Windows;

namespace Inventarizauia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ProizvoditelWindow pr = new ProizvoditelWindow();
            pr.Show();
            this.Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            MestoYstanovkiWindow my = new MestoYstanovkiWindow();
            my.Show();
            this.Close();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            TipEquipmentWindow tq = new TipEquipmentWindow();
            tq.Show();
            this.Close();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            StatusOborudovaniaWindow so = new StatusOborudovaniaWindow();
            so.Show();
            this.Close();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            StatusZauvkaWindow sz = new StatusZauvkaWindow();
            sz.Show();
            this.Close();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            OborudovanieWindow ow = new OborudovanieWindow();
            ow.Show();
            this.Close();
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            SotrudnikiWindow sw = new SotrudnikiWindow();
            sw.Show();
            this.Close();
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            ZauvkaWindow zw = new ZauvkaWindow();
            zw.Show();
            this.Close();
        }

        private void Ellipse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при закрытии окна: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Ellipse_MouseLeftButtonDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

