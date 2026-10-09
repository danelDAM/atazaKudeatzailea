namespace AtazaKudeatzailea.Models
{
    public class Ataza
    {
        public int Id { get; set; }

        public string Titulua { get; set; } = "";

        public string Lehentasuna { get; set; } = "Ertaina";

        public DateTime AzkenEguna { get; set; }

        public bool Eginda { get; set; }
    }
}