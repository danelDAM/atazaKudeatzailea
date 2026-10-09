using AtazaKudeatzailea.Models;
using System.Windows;
using System.Windows.Controls;

namespace AtazaKudeatzailea
{
    public partial class AtazaLeihoa : Window
    {
        public Ataza Ataza { get; private set; }


        public AtazaLeihoa(Ataza ataza)
        {
            InitializeComponent();

            Ataza = ataza;

            txtTitulua.Text = Ataza.Titulua;

            dpAzkenEguna.SelectedDate =
                Ataza.AzkenEguna;

            foreach (ComboBoxItem item in cmbLehentasuna.Items)
            {
                if (item.Content?.ToString() ==
                    Ataza.Lehentasuna)
                {
                    cmbLehentasuna.SelectedItem = item;
                    break;
                }
            }

            if (cmbLehentasuna.SelectedItem == null)
            {
                cmbLehentasuna.SelectedIndex = 1;
            }
        }


        private void Gorde_Click(
            object sender,
            RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTitulua.Text))
            {
                MessageBox.Show(
                    "Titulua derrigorrezkoa da.",
                    "Balidazioa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );

                txtTitulua.Focus();

                return;
            }



            if (dpAzkenEguna.SelectedDate == null)
            {
                MessageBox.Show(
                    "Azken eguna aukeratu behar duzu.",
                    "Balidazioa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );

                return;
            }


            if (dpAzkenEguna.SelectedDate.Value <
                DateTime.Today)
            {
                MessageBox.Show(
                    "Azken eguna ezin da gaur baino lehenagokoa izan.",
                    "Balidazioa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );

                return;
            }



            Ataza.Titulua =
                txtTitulua.Text.Trim();

            Ataza.Lehentasuna =
                ((ComboBoxItem)cmbLehentasuna.SelectedItem)
                .Content
                .ToString()!;

            Ataza.AzkenEguna =
                dpAzkenEguna.SelectedDate.Value;


        

            DialogResult = true;
        }

        private void Utzi_Click(
            object sender,
            RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}