using AtazaKudeatzailea.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace AtazaKudeatzailea
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Ataza> atazak =
            new ObservableCollection<Ataza>();

        private readonly string xmlFitxategia =
            System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data",
                "atazak.xml"
            );


        public MainWindow()
        {
            InitializeComponent();

            dgAtazak.ItemsSource = atazak;

            KargatuXML();
        }


        private void KargatuXML()
        {
            if (!File.Exists(xmlFitxategia))
            {
                return;
            }

            XDocument dokumentua = XDocument.Load(xmlFitxategia);

            if (dokumentua.Root == null)
            {
                return;
            }

            foreach (XElement elementua in dokumentua.Root.Elements("Ataza"))
            {
                string egoera =
                    elementua.Element("Egoera")?.Value ?? "Egin gabe";

                Ataza ataza = new Ataza
                {
                    Id = int.Parse(
                        elementua.Attribute("id")?.Value ?? "0"
                    ),

                    Titulua =
                        elementua.Element("Titulua")?.Value ?? "",

                    Lehentasuna =
                        elementua.Element("Lehentasuna")?.Value
                        ?? "Ertaina",

                    AzkenEguna =
                        DateTime.ParseExact(
                            elementua.Element("AzkenEguna")?.Value
                            ?? DateTime.Today.ToString("yyyy-MM-dd"),
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture
                        ),

                    Eginda = egoera == "Eginda"
                };

                atazak.Add(ataza);
            }
        }


        private void GordeXML()
        {
            XElement erroa = new XElement("Atazak");

            foreach (Ataza ataza in atazak)
            {
                XElement elementua =
                    new XElement(
                        "Ataza",
                        new XAttribute("id", ataza.Id),

                        new XElement(
                            "Titulua",
                            ataza.Titulua
                        ),

                        new XElement(
                            "Lehentasuna",
                            ataza.Lehentasuna
                        ),

                        new XElement(
                            "AzkenEguna",
                            ataza.AzkenEguna.ToString("yyyy-MM-dd")
                        ),

                        new XElement(
                            "Egoera",
                            ataza.Eginda
                                ? "Eginda"
                                : "Egin gabe"
                        )
                    );

                erroa.Add(elementua);
            }

            XDocument dokumentua =
                new XDocument(
                    new XDeclaration(
                        "1.0",
                        "utf-8",
                        "yes"
                    ),
                    erroa
                );

            string? karpeta =
                Path.GetDirectoryName(xmlFitxategia);

            if (karpeta != null)
            {
                Directory.CreateDirectory(karpeta);
            }

            dokumentua.Save(xmlFitxategia);
        }


        private void Berria_Click(
            object sender,
            RoutedEventArgs e)
        {
            int idBerria = 1;

            if (atazak.Count > 0)
            {
                idBerria =
                    atazak.Max(a => a.Id) + 1;
            }

            Ataza atazaBerria = new Ataza
            {
                Id = idBerria,
                Titulua = "",
                Lehentasuna = "Ertaina",
                AzkenEguna = DateTime.Today,
                Eginda = false
            };

            AtazaLeihoa leihoa =
                new AtazaLeihoa(atazaBerria);

            leihoa.Owner = this;

            bool? emaitza = leihoa.ShowDialog();

            if (emaitza == true)
            {
                atazak.Add(leihoa.Ataza);

                GordeXML();
            }
        }


        private void Editatu_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (dgAtazak.SelectedItem is not Ataza ataza)
            {
                MessageBox.Show(
                    "Hautatu ataza bat editatzeko.",
                    "Abisua",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                return;
            }

            Ataza kopia = new Ataza
            {
                Id = ataza.Id,
                Titulua = ataza.Titulua,
                Lehentasuna = ataza.Lehentasuna,
                AzkenEguna = ataza.AzkenEguna,
                Eginda = ataza.Eginda
            };

            AtazaLeihoa leihoa =
                new AtazaLeihoa(kopia);

            leihoa.Owner = this;

            bool? emaitza = leihoa.ShowDialog();

            if (emaitza == true)
            {
                ataza.Titulua =
                    leihoa.Ataza.Titulua;

                ataza.Lehentasuna =
                    leihoa.Ataza.Lehentasuna;

                ataza.AzkenEguna =
                    leihoa.Ataza.AzkenEguna;

                ataza.Eginda =
                    leihoa.Ataza.Eginda;

                dgAtazak.Items.Refresh();

                GordeXML();
            }
        }


        private void Ezabatu_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (dgAtazak.SelectedItem is not Ataza ataza)
            {
                MessageBox.Show(
                    "Hautatu ataza bat ezabatzeko.",
                    "Abisua",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                return;
            }

            MessageBoxResult emaitza =
                MessageBox.Show(
                    "Ziur zaude ataza ezabatu nahi duzula?",
                    "Berrespena",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

            if (emaitza == MessageBoxResult.Yes)
            {
                atazak.Remove(ataza);

                GordeXML();
            }
        }


        private void Irten_Click(
            object sender,
            RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            GordeXML();
        }
    }
}