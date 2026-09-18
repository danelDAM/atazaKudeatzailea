using Microsoft.VisualBasic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _8.ariketa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void exekutatu(object sender, RoutedEventArgs e)
        {
            orainTB.Text = DateTime.Now.ToString();
            gaurTB.Text = DateTime.Now.ToString("dd/MM/yyyy");
            orduaTB.Text = DateTime.Now.ToString("HH:mm:ss");

            datuenBatura(sender, e);
        }

        private void irten(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void garbitu(object sender, RoutedEventArgs e)
        {
            orainTB.Text = "";
            gaurTB.Text = "";
            orduaTB.Text = "";
            baturaTB.Text = "";
            aldeaTB.Text = "";
        }

        private void datuenBatura(object sender, RoutedEventArgs e)
        {
            string meses = Interaction.InputBox(
                "Zenbat hilabete gehitu nahi dituzu?",
                "Datuen batura",
                "0"
            );

            int hilabeteKopurua;

            if (!int.TryParse(meses, out hilabeteKopurua) || hilabeteKopurua < 0)
            {
                MessageBox.Show(
                    "Datu okerra sartu duzu. Mesedez, saiatu berriro.",
                    "Errorea",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                datuenBatura(sender, e);
                return;
            }

            DateTime dataBerria = DateTime.Now.AddMonths(hilabeteKopurua);

            baturaTB.Text = dataBerria.ToString("dd/MM/yyyy");

            TimeSpan diferentzia = dataBerria - DateTime.Now;

            aldeaTB.Text = $"{DateTime.Now:dd/MM/yyyy} eta {dataBerria:dd/MM/yyyy} artean dagoen diferentzia, {diferentzia.Days} egunekoa da";
        }


    }
}